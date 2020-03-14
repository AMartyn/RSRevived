﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Gameplay.AI.GangAI
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Engine;
using djack.RogueSurvivor.Engine.Actions;
using djack.RogueSurvivor.Engine.AI;
using djack.RogueSurvivor.Gameplay.AI.Sensors;
using djack.RogueSurvivor.Gameplay.AI.Tools;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Point = Zaimoni.Data.Vector2D_short;
using Percept = djack.RogueSurvivor.Engine.AI.Percept_<object>;

namespace djack.RogueSurvivor.Gameplay.AI
{
  [Serializable]
  internal class GangAI : OrderableAI
  {
    private static string[] FIGHT_EMOTES = new string[3]
    {
      "Fuck you",
      "Fuck it I'm trapped!",
      "Come on"
    };
    private const int LOS_MEMORY = WorldTime.TURNS_PER_HOUR/3;
    private const int DONT_LEAVE_BEHIND_EMOTE_CHANCE = 50;

    public const LOSSensor.SensingFilter VISION_SEES = LOSSensor.SensingFilter.ACTORS | LOSSensor.SensingFilter.ITEMS;

    private readonly MemorizedSensor<LOSSensor> m_MemLOSSensor = new MemorizedSensor<LOSSensor>(new LOSSensor(VISION_SEES), LOS_MEMORY);
    private readonly ExplorationData m_Exploration = new ExplorationData();

    public GangAI()
    {
    }

    public override bool UsesExplosives { get { return false; } }

    [OnSerializing] private void OptimizeBeforeSaving(StreamingContext context)
    {
      m_MemLOSSensor.Forget(m_Actor);
    }

#nullable enable
    public override List<Percept> UpdateSensors()
    {
      return m_MemLOSSensor.Sense(m_Actor);
    }

    public override HashSet<Point> FOV { get { return m_MemLOSSensor.Sensor.FOV; } }
    public override Dictionary<Location, Actor>? friends_in_FOV { get { return m_MemLOSSensor.Sensor.friends; } }
    public override Dictionary<Location, Actor>? enemies_in_FOV { get { return m_MemLOSSensor.Sensor.enemies; } }
    public override Dictionary<Location, Inventory>? items_in_FOV { get { return m_MemLOSSensor.Sensor.items; } }
#nullable restore
    protected override void SensorsOwnedBy(Actor actor) { m_MemLOSSensor.Sensor.OwnedBy(actor); }

    // return value must contain a {0} placeholder for the target name
    private string LeaderText_NotLeavingBehind(Actor target)
    {
      if (target.IsSleeping) return "patiently waits for {0} to wake up.";
      else if (CanSee(target.Location)) return "Hey {0}! Fucking move!";
      else return "Where is that {0} retard?";
    }

    protected override ActorAction SelectAction(RogueGame game)
    {
      ClearMovePlan();
      BehaviorEquipBestBodyArmor();

      // start item juggling
      if (!BehaviorEquipCellPhone(game) && !BehaviorEquipLight()) {}
      // end item juggling check

      _all = FilterSameMap(UpdateSensors());
      var current = FilterCurrent(_all);    // this tests fast

      m_Actor.Walk();    // alpha 10: don't run by default

      // OrderableAI specific: respond to orders
      if (null != Order) {
        var actorAction = ExecuteOrder(game, Order, _all);
        if (null != actorAction) {
          m_Actor.Activity = Activity.FOLLOWING_ORDER;
          return actorAction;
        }

        SetOrder(null);
      }
      m_Actor.Activity = Activity.IDLE; // backstop

      if (m_Actor.Location!=PrevLocation) m_Exploration.Update(m_Actor.Location);
      InitAICache(_all);

      var old_enemies = FilterEnemies(_all);
      _enemies = SortByGridDistance(FilterCurrent(old_enemies));
      if (null == _enemies) AdviseFriendsOfSafety();

      // New objectives systems
      if (0<Objectives.Count) {
        ActorAction goal_action = null;
        foreach(Objective o in new List<Objective>(Objectives)) {
#if TRACE_SELECTACTION
          if (m_Actor.IsDebuggingTarget && !o.IsExpired)  Logger.WriteLine(Logger.Stage.RUN_MAIN, o.ToString());
#endif
          if (o.IsExpired) Objectives.Remove(o);
          else if (o.UrgentAction(out goal_action)) {
            if (null==goal_action) Objectives.Remove(o);
#if DEBUG
            else if (!goal_action.IsPerformable()) throw new InvalidOperationException("result of UrgentAction should be legal");
#else
            else if (!goal_action.IsPerformable()) Objectives.Remove(o);
#endif
            else return goal_action;
          }
        }
      }

      ActorAction? tmpAction = null;

      // melee risk management check
      // if energy above 50, then we have a free move (range 2 evasion, or range 1/attack), otherwise range 1
      // must be above equip weapon check as we don't want to reload in an avoidably dangerous situation

      // XXX the proper weapon should be calculated like a player....
      // range 1: if melee weapon has a good enough one-shot kill rate, use it
      // any range: of all ranged weapons available, use the weakest one with a good enough one-shot kill rate
      // we may estimate typical damage as 5/8ths of the damage rating for linear approximations
      // use above both for choosing which threat to target, and actual weapon equipping
      // Intermediate data structure: Dictionary<Actor,Dictionary<Item,float>>

      List<Engine.Items.ItemRangedWeapon> available_ranged_weapons = GetAvailableRangedWeapons();

      tmpAction = ManageMeleeRisk(available_ranged_weapons);
#if TRACE_SELECTACTION
      if (m_Actor.IsDebuggingTarget && null!=tmpAction) Logger.WriteLine(Logger.Stage.RUN_MAIN, "managing melee risk: "+tmpAction);
#endif
      if (null != tmpAction) return tmpAction;

      tmpAction = BehaviorEquipWeapon(available_ranged_weapons);
      if (null != tmpAction) return tmpAction;

	  var friends = FilterNonEnemies(_all);
      if (null != _enemies) {
        if (null != friends && Rules.Get.RollChance(50)) {
          tmpAction = BehaviorWarnFriends(friends, FilterNearest(_enemies).Percepted as Actor);
          if (null != tmpAction) return tmpAction;
        }
        tmpAction = BehaviorFightOrFlee(game, ActorCourage.COURAGEOUS, FIGHT_EMOTES, RouteFinder.SpecialActions.JUMP | RouteFinder.SpecialActions.DOORS | RouteFinder.SpecialActions.BREAK | RouteFinder.SpecialActions.PUSH);
        if (null != tmpAction) return tmpAction;
      }
      // at this point, even if enemies are in sight we have no useful direct combat action

      tmpAction = BehaviorUseMedecine(2, 1, 2, 4, 2);
#if TRACE_SELECTACTION
      if (m_Actor.IsDebuggingTarget) Logger.WriteLine(Logger.Stage.RUN_MAIN, "BehaviorUseMedecine ok"); // TRACER
      if (m_Actor.IsDebuggingTarget && null!=tmpAction) Logger.WriteLine(Logger.Stage.RUN_MAIN, "medicating");
#endif
      if (null != tmpAction) return tmpAction;
      tmpAction = BehaviorRestIfTired();
      if (null != tmpAction) return tmpAction;

      if (null != old_enemies && !m_Actor.IsTired) {    // difference between gang and CHAR/soldier is ok here
        tmpAction = BehaviorChargeEnemy(FilterNearest(old_enemies), false, false);
        if (null != tmpAction) return tmpAction;
      }

      // handle food after enemies checks
      tmpAction = BehaviorEatProactively();
      if (null != tmpAction) return tmpAction;

      if (m_Actor.IsHungry) {
        tmpAction = BehaviorEat();
        if (null != tmpAction) return tmpAction;
        if (m_Actor.IsStarving || m_Actor.IsInsane) {
          tmpAction = BehaviorGoEatCorpse(_all);
          if (null != tmpAction) return tmpAction;
        }
      }

      // XXX TurnOnAdjacentGenerators() block would go here, but they're all underground (and inaccessible)

      // while groggy ai may not be up to ranged inventory management, items in reach should still be managed
      tmpAction = InventoryStackTactics();
#if TRACE_SELECTACTION
      if (m_Actor.IsDebuggingTarget && null!=tmpAction) Logger.WriteLine(Logger.Stage.RUN_MAIN, "inventory management failsafe triggered");
#endif
      if (null != tmpAction) return tmpAction;
      tmpAction = BehaviorUseAdjacentStack();
#if TRACE_SELECTACTION
      if (m_Actor.IsDebuggingTarget && null!=tmpAction) Logger.WriteLine(Logger.Stage.RUN_MAIN, "adjacent inventory management triggered");
#endif
      if (null != tmpAction) return tmpAction;

      tmpAction = BehaviorDropUselessItem();
      if (null != tmpAction) return tmpAction;

      if (null == old_enemies && WantToSleepNow) {
        tmpAction = BehaviorNavigateToSleep();
        if (null != tmpAction) return tmpAction;
      }

      if (2<=WantRestoreSAN) {  // intrinsic item rating code for sanity restore is want or higher
        tmpAction = BehaviorUseEntertainment();
        if (null != tmpAction)  return tmpAction;
      }

      tmpAction = BehaviorRangedInventory();
      if (null != tmpAction) return tmpAction;

      if (null == _enemies) {
        // rewriting this to work around a paradoxical bug indicating runtime state corruption
        var mayStealFrom = FilterCurrent(_all).FilterT<Actor>(a =>
        {
          if ((a.Inventory?.IsEmpty ?? true) || IsFriendOf(a)) return false;
          if (!a.IsUnsuspiciousFor(m_Actor)) return HasAnyInterestingItem(a.Inventory);
          game.DoEmote(a, string.Format("moves unnoticed by {0}.", m_Actor.Name));
          return false;
        });
        if (null!= mayStealFrom) {
          // alpha10 make sure to consider only reachable victims
          // gangs can break & push stuff
          FilterOutUnreachablePercepts(ref mayStealFrom, RouteFinder.SpecialActions.ADJ_TO_DEST_IS_GOAL | RouteFinder.SpecialActions.JUMP | RouteFinder.SpecialActions.DOORS | RouteFinder.SpecialActions.BREAK | RouteFinder.SpecialActions.PUSH);
          if (0 < mayStealFrom.Count) {
            Percept victimize = FilterNearest(mayStealFrom);
            Actor target = victimize.Percepted as Actor;
            Item obj = target.Inventory?.GetFirstMatching<Item>(it => IsInterestingItem(it));
            game.DoMakeAggression(m_Actor, target);
            m_Actor.Activity = Activity.CHASING;
            m_Actor.TargetActor = target;
            return new ActionSay(m_Actor, target, string.Format("Hey! That's some nice {0} you have here!", obj.Model.SingleName), RogueGame.Sayflags.IS_IMPORTANT | RogueGame.Sayflags.IS_DANGER); // takes turn for game balance
          }
        }
      }

      tmpAction = BehaviorAttackBarricade();    // gang-specific
      if (null != tmpAction) return tmpAction;

      if (m_Actor.HasLeader && !DontFollowLeader) {
        tmpAction = BehaviorFollowActor(m_Actor.Leader, 1);
        if (null != tmpAction) {
          m_Actor.Activity = Activity.FOLLOWING;
          m_Actor.TargetActor = m_Actor.Leader;
          return tmpAction;
        }
      } else if (m_Actor.CountFollowers < m_Actor.MaxFollowers) {
        var want_leader = friends?.Filter(a => m_Actor.CanTakeLeadOf(a.Percepted));
        FilterOutUnreachablePercepts(ref want_leader, RouteFinder.SpecialActions.DOORS | RouteFinder.SpecialActions.JUMP);
        var target = FilterNearest(want_leader);
        if (target != null) {
          tmpAction = BehaviorLeadActor(target);
          if (null != tmpAction) {
            m_Actor.TargetActor = target.Percepted;
            return tmpAction;
          }
        }
      }

      // critical item memory check goes here

      if (m_Actor.CountFollowers > 0) {
        tmpAction = BehaviorDontLeaveFollowersBehind(3, out Actor target);
        if (null != tmpAction) {
          if (Rules.Get.RollChance(DONT_LEAVE_BEHIND_EMOTE_CHANCE))
            game.DoEmote(m_Actor, string.Format(LeaderText_NotLeavingBehind(target), target.Name));
          m_Actor.Activity = Activity.IDLE;
          return tmpAction;
        }
      }

      // hunt down threats would go here
      // tourism would go here

      tmpAction = BehaviorExplore(m_Exploration);
      if (null != tmpAction) {
        m_Actor.Activity = Activity.IDLE;
        return tmpAction;
      }
      return BehaviorWander(m_Exploration);
    }
  }
}
