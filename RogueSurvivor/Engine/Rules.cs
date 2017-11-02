﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Rules
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

// #define POLICE_NO_QUESTIONS_ASKED

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Engine.Actions;
using djack.RogueSurvivor.Engine.Items;
using djack.RogueSurvivor.Engine.MapObjects;
using djack.RogueSurvivor.Gameplay;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Zaimoni.Data;

namespace djack.RogueSurvivor.Engine
{
  internal class Rules
  {
    public const int INFECTION_LEVEL_1_WEAK = 10;
    public const int INFECTION_LEVEL_2_TIRED = 30;
    public const int INFECTION_LEVEL_3_VOMIT = 50;
    public const int INFECTION_LEVEL_4_BLEED = 75;
    public const int INFECTION_LEVEL_5_DEATH = 100;
    public const int INFECTION_LEVEL_1_WEAK_STA = 24;
    public const int INFECTION_LEVEL_2_TIRED_STA = 24;
    public const int INFECTION_LEVEL_2_TIRED_SLP = 90;
    public const int INFECTION_LEVEL_4_BLEED_HP = 6;
    public const int INFECTION_EFFECT_TRIGGER_CHANCE_1000 = 2;
    public const int UPGRADE_SKILLS_TO_CHOOSE_FROM = 5;
    public const int UNDEAD_UPGRADE_SKILLS_TO_CHOOSE_FROM = 2;
    public static int SKILL_AGILE_DEF_BONUS = 2;
    public static float SKILL_AWAKE_SLEEP_REGEN_BONUS = 0.2f;
    public static float SKILL_CARPENTRY_BARRICADING_BONUS = 0.2f;
    public static int SKILL_CHARISMATIC_TRUST_BONUS = 1;
    public static int SKILL_CHARISMATIC_TRADE_BONUS = 10;
    public static int SKILL_HARDY_HEAL_CHANCE_BONUS = 1;
    public static int SKILL_LIGHT_FEET_TRAP_BONUS = 5;
    public static int SKILL_LIGHT_SLEEPER_WAKEUP_CHANCE_BONUS = 10;
    public static float SKILL_MEDIC_BONUS = 0.2f;
    public static int SKILL_MEDIC_REVIVE_BONUS = 10;
    public const int SKILL_MEDIC_LEVEL_FOR_REVIVE_EST = 1;
    public static int SKILL_NECROLOGY_CORPSE_BONUS = 4;
    public const int SKILL_NECROLOGY_LEVEL_FOR_INFECTION = 3;
    public const int SKILL_NECROLOGY_LEVEL_FOR_RISE = 5;
    public static float SKILL_STRONG_PSYCHE_LEVEL_BONUS = 0.15f;
    public static float SKILL_STRONG_PSYCHE_ENT_BONUS = 0.15f;
    public static int SKILL_UNSUSPICIOUS_BONUS = 25;
    public const int UNSUSPICIOUS_BAD_OUTFIT_PENALTY = 50;
    public const int UNSUSPICIOUS_GOOD_OUTFIT_BONUS = 50;
    public static int SKILL_ZAGILE_DEF_BONUS = 2;
    public static float SKILL_ZEATER_REGEN_BONUS = 0.2f;
    public static int SKILL_ZLIGHT_FEET_TRAP_BONUS = 3;
    public static int SKILL_ZGRAB_CHANCE = 2;
    public static float SKILL_ZINFECTOR_BONUS = 0.1f;
    public const int BASE_ACTION_COST = 100;
    public const int BASE_SPEED = 100;
    public const int STAMINA_COST_RUNNING = 4;
    public const int STAMINA_REGEN_PER_TURN = 2;
    public const int STAMINA_COST_JUMP = 8;
    public const int STAMINA_COST_MELEE_ATTACK = 8;
    public const int STAMINA_COST_MOVE_DRAGGED_CORPSE = 8;
    public const int JUMP_STUMBLE_CHANCE = 25;
    public const int JUMP_STUMBLE_ACTION_COST = 100;
    public const int BARRICADING_MAX = 80;
    public const int MELEE_WEAPON_BREAK_CHANCE = 1;
    public const int MELEE_WEAPON_FRAGILE_BREAK_CHANCE = 3;
    public const int FIREARM_JAM_CHANCE_NO_RAIN = 1;
    public const int FIREARM_JAM_CHANCE_RAIN = 3;
    public const int BODY_ARMOR_BREAK_CHANCE = 2;
    public const int FOOD_BASE_POINTS = 2*Actor.FOOD_HUNGRY_LEVEL;
    public const int ROT_BASE_POINTS = 2*Actor.ROT_HUNGRY_LEVEL;
    public const int SLEEP_BASE_POINTS = 2*Actor.SLEEP_SLEEPY_LEVEL;
    public const int SANITY_BASE_POINTS = 4*WorldTime.TURNS_PER_DAY;
    public const int SANITY_UNSTABLE_LEVEL = 2*WorldTime.TURNS_PER_DAY;
    public const int SANITY_NIGHTMARE_CHANCE = 2;
    public const int SANITY_NIGHTMARE_SLP_LOSS = 2*WorldTime.TURNS_PER_HOUR;
    public const int SANITY_NIGHTMARE_SAN_LOSS = WorldTime.TURNS_PER_HOUR;
    public const int SANITY_INSANE_ACTION_CHANCE = 5;
    public const int SANITY_HIT_BUTCHERING_CORPSE = WorldTime.TURNS_PER_HOUR;
    public const int SANITY_HIT_UNDEAD_EATING_CORPSE = 2*WorldTime.TURNS_PER_HOUR;
    public const int SANITY_HIT_LIVING_EATING_CORPSE = 4*WorldTime.TURNS_PER_HOUR;
    public const int SANITY_HIT_EATEN_ALIVE = 4*WorldTime.TURNS_PER_HOUR;
    public const int SANITY_HIT_ZOMBIFY = 2*WorldTime.TURNS_PER_HOUR;
    public const int SANITY_HIT_BOND_DEATH = 8*WorldTime.TURNS_PER_HOUR;
    public const int SANITY_RECOVER_KILL_UNDEAD = 2*WorldTime.TURNS_PER_HOUR;
    public const int SANITY_RECOVER_BOND_CHANCE = 5;
    public const int SANITY_RECOVER_BOND = 30;
    public const int FOOD_STARVING_DEATH_CHANCE = 5;
    public const int FOOD_EXPIRED_VOMIT_CHANCE = 25;
    public const int FOOD_VOMIT_STA_COST = 100;
    public const int ROT_STARVING_HP_CHANCE = 5;
    public const int ROT_HUNGRY_SKILL_CHANCE = 5;
    public const int SLEEP_EXHAUSTION_COLLAPSE_CHANCE = 5;
    private const int SLEEP_COUCH_SLEEPING_REGEN = 6;
    private const int SLEEP_NOCOUCH_SLEEPING_REGEN = 4;
    public const int SLEEP_ON_COUCH_HEAL_CHANCE = 5;
    public const int SLEEP_HEAL_HITPOINTS = 2;
    public const int LOUD_NOISE_RADIUS = WorldTime.TURNS_PER_HOUR/6;    // space-time scales; value 5 for 30 turns/hour
    private const int LOUD_NOISE_BASE_WAKEUP_CHANCE = 10;
    private const int LOUD_NOISE_DISTANCE_BONUS = 10;
    public const int VICTIM_DROP_GENERIC_ITEM_CHANCE = 50;
    public const int VICTIM_DROP_AMMOFOOD_ITEM_CHANCE = 100;
    public const int IMPROVED_WEAPONS_FROM_BROKEN_WOOD_CHANCE = 25;
    public const float RAPID_FIRE_FIRST_SHOT_ACCURACY = 0.5f;
    public const float RAPID_FIRE_SECOND_SHOT_ACCURACY = 0.3f;
    public const int ZTRACKINGRADIUS = 6;
    public const int DEFAULT_ACTOR_WEIGHT = 10;
    public const int FIRE_RAIN_TEST_CHANCE = 1;
    public const int FIRE_RAIN_PUT_OUT_CHANCE = 10;
    public const int TRUST_NEUTRAL = 0;
    public const int TRUST_MIN = -12*WorldTime.TURNS_PER_HOUR;
    public const int TRUST_MAX = 2*WorldTime.TURNS_PER_DAY;
    public const int TRUST_BASE_INCREASE = 1;
    public const int TRUST_GOOD_GIFT_INCREASE = 3*WorldTime.TURNS_PER_HOUR;
    public const int TRUST_MISC_GIFT_INCREASE = WorldTime.TURNS_PER_HOUR/3;
    public const int TRUST_GIVE_ITEM_ORDER_PENALTY = -WorldTime.TURNS_PER_HOUR;
    public const int TRUST_LEADER_KILL_ENEMY = 3*WorldTime.TURNS_PER_HOUR;
    public const int TRUST_REVIVE_BONUS = 12*WorldTime.TURNS_PER_HOUR;
    public const int MURDERER_SPOTTING_BASE_CHANCE = 5;
    public const int MURDERER_SPOTTING_DISTANCE_PENALTY = 1;
    public const int MURDER_SPOTTING_MURDERCOUNTER_BONUS = 5;
    private const float INFECTION_BASE_FACTOR = 1f;
    private const int CORPSE_ZOMBIFY_BASE_CHANCE = 0;
    private const int CORPSE_ZOMBIFY_DELAY = 6*WorldTime.TURNS_PER_HOUR;
    private const float CORPSE_ZOMBIFY_INFECTIONP_FACTOR = 1f;
    private const float CORPSE_ZOMBIFY_NIGHT_FACTOR = 2f;
    private const float CORPSE_ZOMBIFY_DAY_FACTOR = 0.01f;
    private const float CORPSE_ZOMBIFY_TIME_FACTOR = 0.001388889f;
    private const float CORPSE_EATING_NUTRITION_FACTOR = 10f;
    private const float CORPSE_EATING_INFECTION_FACTOR = 0.1f;
    private const float CORPSE_DECAY_PER_TURN = 0.005555556f;   // 1/180 per turn
    public const int GIVE_RARE_ITEM_DAY = 7;
    public const int GIVE_RARE_ITEM_CHANCE = 5;
    private readonly DiceRoller m_DiceRoller;

    public DiceRoller DiceRoller {
      get {
        return m_DiceRoller;
      }
    }

    public Rules(DiceRoller diceRoller)
    {
#if DEBUG
      if (null == diceRoller) throw new ArgumentNullException(nameof(diceRoller));
#endif
      m_DiceRoller = diceRoller;
    }

    public int Roll(int min, int max)
    {
      return m_DiceRoller.Roll(min, max);
    }

    public bool RollChance(int chance)
    {
      return m_DiceRoller.RollChance(chance);
    }

#if DEAD_FUNC
    public float RollFloat()
    {
      return m_DiceRoller.RollFloat();
    }

    public float Randomize(float value, float deviation)
    {
      float num = deviation / 2f;
      return (float) ((double) value - (double) num * (double)m_DiceRoller.RollFloat() + (double) num * (double)m_DiceRoller.RollFloat());
    }

    public int RollX(Map map)
    {
      if (map == null)
        throw new ArgumentNullException("map");
      return m_DiceRoller.Roll(0, map.Width);
    }

    public int RollY(Map map)
    {
      if (map == null)
        throw new ArgumentNullException("map");
      return m_DiceRoller.Roll(0, map.Height);
    }
#endif

    public T Choose<T>(IEnumerable<T> src) {
      int n = (src?.Count() ?? 0);
      if (0 >= n) throw new ArgumentNullException(nameof(src));
      n = m_DiceRoller.Roll(0, n);
      foreach(var x in src) if (0 >= n--) return x;
      throw new ArgumentNullException(nameof(src)); // unreachable with a sufficiently correct compiler
    }

    public Direction RollDirection()
    {
      return Direction.COMPASS[m_DiceRoller.Roll(0, 8)];
    }

    public int RollSkill(int skillValue)
    {
      if (skillValue <= 0)
        return 0;
      return (m_DiceRoller.Roll(0, skillValue + 1) + m_DiceRoller.Roll(0, skillValue + 1)) / 2;
    }

    private static int _Average(int x, int y) { return x+y/2; }

    public DenormalizedProbability<int> SkillProbabilityDistribution(int skillValue)
    {
      if (0 >= skillValue) return ConstantDistribution<int>.Get(0);
      DenormalizedProbability<int> sk_prob = UniformDistribution.Get(0,skillValue);
      return DenormalizedProbability<int>.Apply(sk_prob*sk_prob,_Average);  // XXX \todo cache this
    }

    public int RollDamage(int damageValue)
    {
      if (damageValue <= 0) return 0;
      return m_DiceRoller.Roll(damageValue / 2, damageValue + 1);
    }

    public bool CanActorPutItemIntoContainer(Actor actor, Point position)
    {
      return CanActorPutItemIntoContainer(actor, position, out string reason);
    }

    public bool CanActorPutItemIntoContainer(Actor actor, Point position, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      MapObject mapObjectAt = actor.Location.Map.GetMapObjectAt(position);
      if (mapObjectAt == null || !mapObjectAt.IsContainer)
      {
        reason = "object is not a container";
        return false;
      }
      if (!actor.Model.Abilities.HasInventory || !actor.Model.Abilities.CanUseMapObjects || actor.Inventory == null)
      {
        reason = "cannot take an item";
        return false;
      }
      Inventory itemsAt = actor.Location.Map.GetItemsAt(position);
      if (null != itemsAt && itemsAt.IsFull)
      {
        reason = "container is full";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorEatFoodOnGround(Actor actor, Item it, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (it == null)
        throw new ArgumentNullException("item");
      if (!(it is ItemFood))
      {
        reason = "not food";
        return false;
      }
      if (!actor.Location.Items?.Contains(it) ?? true)
      {
        reason = "item not here";
        return false;
      }
      reason = "";
      return true;
    }

    private static ActorAction IsBumpableFor(Actor actor, Map map, int x, int y, out string reason)
    {
#if DEBUG
      if (null == map) throw new ArgumentNullException(nameof(map));
      if (null == actor) throw new ArgumentNullException(nameof(actor));
#endif
      Point point = new Point(x, y);
      reason = "";
      Location loc = new Location(map,point);

      ActorCourage courage = (actor.Controller as Gameplay.AI.OrderableAI)?.Directives.Courage ?? ActorCourage.COURAGEOUS;  // need this to be inoperative for z
      bool imStarvingOrCourageous = actor.IsStarving || ActorCourage.COURAGEOUS == courage;
      if (!imStarvingOrCourageous) {
        int trapsMaxDamage = loc.Map.TrapsMaxDamageAt(loc.Position);
        if (trapsMaxDamage > 0 && trapsMaxDamage >= actor.HitPoints) {
          reason = "deathtrapped";
          return null;
        }
      }

      Actor actorAt = map.GetActorAtExt(point);
      if (actorAt != null) {
        if (actor.IsEnemyOf(actorAt)) {
          return (actor.CanMeleeAttack(actorAt, out reason) ? new ActionMeleeAttack(actor, actorAt) : null);
        }
		// player as leader should be able to switch with player as follower
		// NPCs shouldn't be leading players anyway
        if ((actor.IsPlayer || !actorAt.IsPlayer) && actor.CanSwitchPlaceWith(actorAt, out reason))
          return new ActionSwitchPlace(actor, actorAt);
        return (actor.CanChatWith(actorAt, out reason) ? new ActionChat(actor, actorAt) : null);
      }
      if (!map.IsInBounds(x, y)) {
	    return (actor.CanLeaveMap(point, out reason) ? new ActionLeaveMap(actor, point) : null);
      }
      ActionMoveStep actionMoveStep = new ActionMoveStep(actor, point);
      if (actionMoveStep.IsLegal()) {
        reason = "";
        return actionMoveStep;
      }
      reason = actionMoveStep.FailReason;
      MapObject mapObjectAt = map.GetMapObjectAt(point);
      if (mapObjectAt != null) {
        if (mapObjectAt is DoorWindow door) {
          if (door.IsClosed) {
            if (actor.CanOpen(door, out reason)) return new ActionOpenDoor(actor, door);
            if (actor.CanBash(door, out reason)) return new ActionBashDoor(actor, door);
            return null;
          }
          // covers barricaded broken windows...otherwise redundant.
          if (door.BarricadePoints > 0) {
            // Z will bash barricaded doors but livings won't, except for specific overrides
            // this does conflict with the tourism behavior
            if (actor.CanBash(door, out reason)) return new ActionBashDoor(actor, door);
            reason = "cannot bash the barricade";
            return null;
          }
        }
        if (actor.CanGetFromContainer(point, out reason)) {
          ActionGetFromContainer tmp = new ActionGetFromContainer(actor, point);
          if (actor.IsPlayer || ((actor.Controller as Gameplay.AI.ObjectiveAI)?.IsInterestingItem(tmp.Item) ?? false)) return tmp;
        }
        // only Z want to break arbitrary objects; thus the guard clause
        if (actor.Model.Abilities.CanBashDoors && actor.CanBreak(mapObjectAt, out reason))
          return new ActionBreak(actor, mapObjectAt);
        if (mapObjectAt is PowerGenerator powGen) {
          if (powGen.IsOn) {
            Item tmp = actor.GetEquippedItem(DollPart.LEFT_HAND);   // normal lights and trackers
            if (tmp != null && actor.CanRecharge(tmp, out reason))
              return new ActionRechargeItemBattery(actor, tmp);
            tmp = actor.GetEquippedItem(DollPart.RIGHT_HAND);   // formal correctness
            if (tmp != null && actor.CanRecharge(tmp, out reason))
              return new ActionRechargeItemBattery(actor, tmp);
            tmp = actor.GetEquippedItem(DollPart.HIP_HOLSTER);   // the police tracker
            if (tmp != null && actor.CanRecharge(tmp, out reason))
              return new ActionRechargeItemBattery(actor, tmp);
          }
          return (actor.CanSwitch(powGen, out reason) ? new ActionSwitchPowerGenerator(actor, powGen) : null);
        }
      }
      return null;
    }

    public static ActorAction IsBumpableFor(Actor actor, Location location)
    {
      return IsBumpableFor(actor, location, out string reason);
    }

    public static ActorAction IsBumpableFor(Actor actor, Location location, out string reason)
    {
      return IsBumpableFor(actor, location.Map, location.Position.X, location.Position.Y, out reason);
    }

    // Pathfindability is not quite the same as bumpability
    // * ok to break barricaded doors on fastest path
    // * only valid for subclasses of ObjectiveAI/OrderableAI (which can pathfind in the first place).
    // * ok to push objects aside
    // * not ok to chat as a time-cost action (could be re-implemented)
    private static ActorAction IsPathableFor(Actor actor, Map map, int x, int y, out string reason)
    {
#if DEBUG
      if (null == map) throw new ArgumentNullException(nameof(map));
      if (null == actor) throw new ArgumentNullException(nameof(actor));
      if (!(actor.Controller is Gameplay.AI.ObjectiveAI)) throw new InvalidOperationException("!(actor.Controller is Gameplay.AI.ObjectiveAI)");
#endif
      Gameplay.AI.ObjectiveAI ai = actor.Controller as Gameplay.AI.ObjectiveAI;
      Point point = new Point(x, y);
      Location loc = new Location(map,point);
      reason = "";

      ActorCourage courage = (actor.Controller as Gameplay.AI.OrderableAI)?.Directives.Courage ?? ActorCourage.CAUTIOUS;
      bool imStarvingOrCourageous = actor.IsStarving || ActorCourage.COURAGEOUS == courage;
      if (!imStarvingOrCourageous) {
        int trapsMaxDamage = loc.Map.TrapsMaxDamageAt(loc.Position);
        if (trapsMaxDamage > 0 && trapsMaxDamage >= actor.HitPoints) {
          reason = "deathtrapped";
          return null;
        }
      }

      if (!map.IsInBounds(x, y)) {
	    return (actor.CanLeaveMap(point, out reason) ? new ActionLeaveMap(actor, point) : null);
      }
      ActionMoveStep actionMoveStep = new ActionMoveStep(actor, point);
      if (loc.IsWalkableFor(actor, out reason)) {
        reason = "";
        return actionMoveStep;
      }

      // only have to be completely accurate for adjacent squares
      if (!Rules.IsAdjacent(actor.Location, loc)) {
        if ("not enough stamina to jump"==actionMoveStep.FailReason) return actionMoveStep;
        if ("someone is there"==actionMoveStep.FailReason) return actionMoveStep;
      }

      reason = actionMoveStep.FailReason;
      Actor actorAt = map.GetActorAt(point);
      if (actorAt != null) {
        if (actor.IsEnemyOf(actorAt)) {
          return (actor.CanMeleeAttack(actorAt, out reason) ? new ActionMeleeAttack(actor, actorAt) : null);
        }
		// player as leader should be able to switch with player as follower
		// NPCs shouldn't be leading players anyway
        if ((actor.IsPlayer || !actorAt.IsPlayer) && actor.CanSwitchPlaceWith(actorAt, out reason))
          return new ActionSwitchPlace(actor, actorAt);
        // no chat when pathfinding
        // but it is ok to shove other actors
        if (actor.AbleToPush && actor.CanShove(actorAt)) {
           // at least 2 destinations: ok (1 ok if adjacent)
           // better to push to non-adjacent when pathing
           // we are adjacent due to the early-escape above
           Dictionary<Point,Direction> push_dest = actorAt.ShoveDestinations;

           bool push_legal = 1<=push_dest.Count;
           if (push_legal) {
             Dictionary<Point, int> self_block = ai.MovePlanIf(actorAt.Location.Position);

             // function target
             List<KeyValuePair<Point, Direction>> candidates = null;
             IEnumerable<KeyValuePair<Point, Direction>> candidates_2 = push_dest.Where(pt => !Rules.IsAdjacent(actor.Location.Position, pt.Key));
             IEnumerable<KeyValuePair<Point, Direction>> candidates_1 = push_dest.Where(pt => Rules.IsAdjacent(actor.Location.Position, pt.Key));
             IEnumerable< KeyValuePair < Point, Direction >> test = (null != self_block ? candidates_2.Where(pt => !self_block.ContainsKey(pt.Key)) : candidates_2);
             if (test.Any()) candidates = test.ToList();
             else if (2<=candidates_2.Count()) candidates = candidates_2.ToList();
             if (null == candidates && candidates_1.Any()) {
               test = (null != self_block ? candidates_1.Where(pt => !self_block.ContainsKey(pt.Key)) : candidates_1);
               if (test.Any()) candidates = test.ToList();
               else candidates = candidates_1.ToList();
             } else candidates = candidates_2.ToList();
             // end function target

             return new ActionShove(actor,actorAt,candidates[RogueForm.Game.Rules.Roll(0,candidates.Count)].Value);
           }
        }
        // consider re-legalizing chat here
        return null;
      }
      MapObject mapObjectAt = map.GetMapObjectAt(point);
      if (mapObjectAt != null) {
        DoorWindow door = mapObjectAt as DoorWindow;
        if (door != null) {
          if (door.BarricadePoints > 0) {
            // pathfinding livings will break barricaded doors (they'll prefer to go around it)
            if (actor.CanBash(door, out reason)) return new ActionBashDoor(actor, door);
            if (actor.CanBreak(door, out reason)) return new ActionBreak(actor, door);
            reason = "cannot bash the barricade";
            return null;
          }
          if (door.IsClosed) {
            if (actor.CanOpen(door, out reason)) return new ActionOpenDoor(actor, door);
            if (actor.CanBash(door, out reason)) return new ActionBashDoor(actor, door);
            return null;
          }
        }
        if (actor.CanGetFromContainer(point, out reason)) {
          ActionGetFromContainer tmp = new ActionGetFromContainer(actor, point);
          if ((actor.Controller as Gameplay.AI.ObjectiveAI).IsInterestingItem(tmp.Item)) return tmp;
        }
        // only Z want to break arbitrary objects; thus the guard clause
        if (actor.Model.Abilities.CanBashDoors && actor.CanBreak(mapObjectAt, out reason))
          return new ActionBreak(actor, mapObjectAt);

        // pushing is very bad for bumping, but ok for pathing
        if (actor.AbleToPush && actor.CanPush(mapObjectAt)) {
           // at least 2 destinations: ok (1 ok if adjacent)
           // better to push to non-adjacent when pathing
           Dictionary<Point,Direction> push_dest = map.ValidDirections(mapObjectAt.Location.Position, (m, pt) => mapObjectAt.CanPushTo(pt));

           bool is_adjacent = Rules.IsAdjacent(actor.Location, mapObjectAt.Location);
           bool push_legal = (is_adjacent ? 1 : 2)<=push_dest.Count;
           if (push_legal) {
             if (is_adjacent) {
               Dictionary<Point, int> self_block = ai.MovePlanIf(mapObjectAt.Location.Position);

               // function target
               List<KeyValuePair<Point, Direction>> candidates = null;
               IEnumerable<KeyValuePair<Point, Direction>> candidates_2 = push_dest.Where(pt => !Rules.IsAdjacent(actor.Location.Position, pt.Key));
               IEnumerable<KeyValuePair<Point, Direction>> candidates_1 = push_dest.Where(pt => Rules.IsAdjacent(actor.Location.Position, pt.Key));
               IEnumerable< KeyValuePair < Point, Direction >> test = (null != self_block ? candidates_2.Where(pt => !self_block.ContainsKey(pt.Key)) : candidates_2);
               if (test.Any()) candidates = test.ToList();
               else if (2<=candidates_2.Count()) candidates = candidates_2.ToList();
               if (null == candidates && candidates_1.Any()) {
                 test = (null != self_block ? candidates_1.Where(pt => !self_block.ContainsKey(pt.Key)) : candidates_1);
                 if (test.Any()) candidates = test.ToList();
                 else candidates = candidates_1.ToList();
               } else candidates = candidates_2.ToList();
               // end function target

               return new ActionPush(actor,mapObjectAt,candidates[RogueForm.Game.Rules.Roll(0,candidates.Count)].Value);
             }
             // placeholder
             List<Direction> candidate_dirs = push_dest.Values.ToList();
             return new ActionPush(actor,mapObjectAt,candidate_dirs[RogueForm.Game.Rules.Roll(0,candidate_dirs.Count)]);
           }
        }

        if (mapObjectAt is PowerGenerator powGen) {
          if (powGen.IsOn) {
            Item tmp = actor.GetEquippedItem(DollPart.LEFT_HAND);   // normal lights and trackers
            if (tmp != null && actor.CanRecharge(tmp, out reason))
              return new ActionRechargeItemBattery(actor, tmp);
            tmp = actor.GetEquippedItem(DollPart.RIGHT_HAND);   // formal correctness
            if (tmp != null && actor.CanRecharge(tmp, out reason))
              return new ActionRechargeItemBattery(actor, tmp);
            tmp = actor.GetEquippedItem(DollPart.HIP_HOLSTER);   // the police tracker
            if (tmp != null && actor.CanRecharge(tmp, out reason))
              return new ActionRechargeItemBattery(actor, tmp);
          }
          return (actor.CanSwitch(powGen, out reason) ? new ActionSwitchPowerGenerator(actor, powGen) : null);
        }
      }
      return null;
    }

    public static ActorAction IsPathableFor(Actor actor, Location location)
    {
      return IsPathableFor(actor, location, out string reason);
    }

    public static ActorAction IsPathableFor(Actor actor, Location location, out string reason)
    {
      return IsPathableFor(actor, location.Map, location.Position.X, location.Position.Y, out reason);
    }

    public int ActorDamageVsCorpses(Actor a)
    {
      return a.CurrentMeleeAttack.DamageValue / 2 + Rules.SKILL_NECROLOGY_CORPSE_BONUS * a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.NECROLOGY);
    }

    public bool CanActorEatCorpse(Actor actor, Corpse corpse, out string reason)
    {
      if (actor == null) throw new ArgumentNullException("actor");
      if (corpse == null) throw new ArgumentNullException("corpse");
      if (!actor.CanEatCorpse)
      {
        reason = "not starving or insane";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorButcherCorpse(Actor actor, Corpse corpse)
    {
      return CanActorButcherCorpse(actor, corpse, out string reason);
    }

    public bool CanActorButcherCorpse(Actor actor, Corpse corpse, out string reason)
    {
      if (actor == null) throw new ArgumentNullException("actor");
      if (corpse == null) throw new ArgumentNullException("corpse");
      if (actor.IsTired) {
        reason = "tired";
        return false;
      }
      if (corpse.Position != actor.Location.Position || !actor.Location.Map.Has(corpse)) {
        reason = "not in same location";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorStartDragCorpse(Actor actor, Corpse corpse)
    {
      return CanActorStartDragCorpse(actor, corpse, out string reason);
    }

    public bool CanActorStartDragCorpse(Actor actor, Corpse corpse, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (corpse == null)
        throw new ArgumentNullException("corpse");
      if (corpse.IsDragged)
      {
        reason = "corpse is already being dragged";
        return false;
      }
      if (actor.IsTired)
      {
        reason = "tired";
        return false;
      }
      if (corpse.Position != actor.Location.Position || !actor.Location.Map.Has(corpse))
      {
        reason = "not in same location";
        return false;
      }
      if (actor.DraggedCorpse != null)
      {
        reason = "already dragging a corpse";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorStopDragCorpse(Actor actor, Corpse corpse)
    {
      return CanActorStopDragCorpse(actor, corpse, out string reason);
    }

    public bool CanActorStopDragCorpse(Actor actor, Corpse corpse, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (corpse == null)
        throw new ArgumentNullException("corpse");
      if (corpse.DraggedBy != actor)
      {
        reason = "not dragging this corpse";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorReviveCorpse(Actor actor, Corpse corpse)
    {
      return CanActorReviveCorpse(actor, corpse, out string reason);
    }

    public bool CanActorReviveCorpse(Actor actor, Corpse corpse, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (corpse == null)
        throw new ArgumentNullException("corpse");
      if (actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.MEDIC) == 0)
      {
        reason = "lack medic skill";
        return false;
      }
      if (corpse.Position != actor.Location.Position)
      {
        reason = "not there";
        return false;
      }
      if (corpse.RotLevel > 0)
      {
        reason = "corpse not fresh";
        return false;
      }
      if (corpse.IsDragged)
      {
        reason = "dragged corpse";
        return false;
      }
      if (!actor.Inventory.Has(GameItems.IDs.MEDICINE_MEDIKIT))
      {
        reason = "no medikit";
        return false;
      }
      reason = "";
      return true;
    }

    // These two somewhat counter-intuitively consider "same location" as adjacent
    public static bool IsAdjacent(Location a, Location b)
    {
      
      if (a.Map != b.Map) {
        Location? test = a.Map.Denormalize(b);
        if (null == test) return false;
        b = test.Value;
      }
      return Rules.IsAdjacent(a.Position, b.Position);
    }

    public static bool IsAdjacent(Point pA, Point pB)
    {
      if (Math.Abs(pA.X - pB.X) < 2)
        return Math.Abs(pA.Y - pB.Y) < 2;
      return false;
    }

    // L-infinity metric i.e. distance in moves
    public static int GridDistance(Point pA, int bX, int bY)
    {
      return Math.Max(Math.Abs(pA.X - bX), Math.Abs(pA.Y - bY));
    }

    public static int GridDistance(Point pA, Point pB)
    {
      return Math.Max(Math.Abs(pA.X - pB.X), Math.Abs(pA.Y - pB.Y));
    }

    public static int GridDistance(Location locA, Location locB)
    {
      if (null == locA.Map) return int.MaxValue;
      if (null == locB.Map) return int.MaxValue;
      if (locA.Map==locB.Map) return GridDistance(locA.Position,locB.Position);
      Location? tmp = locA.Map.Denormalize(locB);
      if (null == tmp) return int.MaxValue;
      return GridDistance(locA.Position,tmp.Value.Position);
    }

    // Euclidean plane distance
    public static double StdDistance(Point from, Point to)
    {
      int num1 = to.X - from.X;
      int num2 = to.Y - from.Y;
      return Math.Sqrt((double) (num1 * num1 + num2 * num2));
    }

    public static double StdDistance(Point v)
    {
      return Math.Sqrt((double) (v.X * v.X + v.Y * v.Y));
    }

    public static bool IsMurder(Actor killer, Actor victim)
    {
      if (null == killer) return false;
      if (null == victim) return false;
      if (victim.Model.Abilities.IsUndead) return false;
      if (killer.Model.Abilities.IsLawEnforcer && victim.MurdersCounter > 0) return false;
#if POLICE_NO_QUESTIONS_ASKED
      if (killer.Model.Abilities.IsLawEnforcer && killer.Threats.IsThreat(victim)) return false;
#endif
      if (killer.Faction.IsEnemyOf(victim.Faction)) return false;

      // If your leader is a cop i.e. First Class Citizen, killing his enemies should not trigger murder charges.
      Actor killer_leader = killer.LiveLeader;
      if (null != killer_leader && killer_leader.Model.Abilities.IsLawEnforcer) {
        if (killer_leader.IsEnemyOf(victim)) return false;
        if (victim.IsSelfDefenceFrom(killer.Leader)) return false;
      }

      // Framed for murder.  Since this is an apocalypse, self-defence doesn't count no matter what the law was pre-apocalypse
      if (victim.Model.Abilities.IsLawEnforcer) return true;
      if (victim.HasLeader && victim.Leader.Model.Abilities.IsLawEnforcer) return true;

      // resume old definition
      if (killer.IsSelfDefenceFrom(victim)) return false;

      if (killer_leader?.IsSelfDefenceFrom(victim) ?? false) return false;
      if (killer.CountFollowers > 0 && !killer.Followers.Any(fo => fo.IsSelfDefenceFrom(victim))) return false;
      return true;
    }

    public static int ActorSanRegenValue(Actor actor, int baseValue)
    {
      int num = (int) ((double) baseValue * (double) Rules.SKILL_STRONG_PSYCHE_ENT_BONUS * (double) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.STRONG_PSYCHE));
      return baseValue + num;
    }

    public static int ActorSleepRegen(Actor actor, bool isOnCouch)
    {
      int num1 = isOnCouch ? SLEEP_COUCH_SLEEPING_REGEN : SLEEP_NOCOUCH_SLEEPING_REGEN;
      int num2 = (int) ((double) num1 * (double) Rules.SKILL_AWAKE_SLEEP_REGEN_BONUS * (double) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.AWAKE));
      return num1 + num2;
    }

    public static int ActorDisturbedLevel(Actor actor)
    {
      return (int) ((double)SANITY_UNSTABLE_LEVEL * (1.0 - (double) Rules.SKILL_STRONG_PSYCHE_LEVEL_BONUS * (double) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.STRONG_PSYCHE)));
    }

    public static Defence ActorDefence(Actor actor, Defence baseDefence)
    {
      if (actor.IsSleeping) return new Defence(0, 0, 0);
      int num1 = Rules.SKILL_AGILE_DEF_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.AGILE) + Rules.SKILL_ZAGILE_DEF_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_AGILE);
      float num2 = (float) (baseDefence.Value + num1);
      if (actor.IsExhausted) num2 /= 2f;
      else if (actor.IsSleepy) num2 *= 0.75f;
      return new Defence((int) num2, baseDefence.Protection_Hit, baseDefence.Protection_Shot);
    }

    public static int ActorMedicineEffect(Actor actor, int baseEffect)
    {
      int num = (int) Math.Ceiling((double) Rules.SKILL_MEDIC_BONUS * (double) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.MEDIC) * (double) baseEffect);
      return baseEffect + num;
    }

    public static int ActorHealChanceBonus(Actor actor)
    {
      return Rules.SKILL_HARDY_HEAL_CHANCE_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.HARDY);
    }

    public static int ActorBarricadingPoints(Actor actor, int baseBarricadingPoints)
    {
      int num = (int) ((double) baseBarricadingPoints * (double) Rules.SKILL_CARPENTRY_BARRICADING_BONUS * (double) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.CARPENTRY));
      return baseBarricadingPoints + num;
    }

    public static int ActorLoudNoiseWakeupChance(Actor actor, int noiseDistance)
    {
      return 10 + Rules.SKILL_LIGHT_SLEEPER_WAKEUP_CHANCE_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.LIGHT_SLEEPER) + Math.Max(0, (LOUD_NOISE_RADIUS - noiseDistance) * 10);
    }

    public static int ActorTrustIncrease(Actor actor)
    {
      return 1 + Rules.SKILL_CHARISMATIC_TRUST_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.CHARISMATIC);
    }

    public static int ActorCharismaticTradeChance(Actor actor)
    {
      return Rules.SKILL_CHARISMATIC_TRADE_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.CHARISMATIC);
    }

    public static int ActorUnsuspicousChance(Actor observer, Actor actor)
    {
      int num1 = SKILL_UNSUSPICIOUS_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.UNSUSPICIOUS);
      int num2 = 0;
      if (actor.GetEquippedItem(DollPart.TORSO) is ItemBodyArmor armor) {
        if (observer.Faction.ID == (int) GameFactions.IDs.ThePolice) {
          if (armor.IsHostileForCops())
            num2 -= UNSUSPICIOUS_BAD_OUTFIT_PENALTY;
          else if (armor.IsFriendlyForCops())
            num2 += UNSUSPICIOUS_GOOD_OUTFIT_BONUS;
        } else if (observer.Faction.ID == (int) GameFactions.IDs.TheBikers) {
          if (armor.IsHostileForBiker(observer.GangID))
            num2 -= UNSUSPICIOUS_BAD_OUTFIT_PENALTY;
          else if (armor.IsFriendlyForBiker(observer.GangID))
            num2 += UNSUSPICIOUS_GOOD_OUTFIT_BONUS;
        }
      }
      return num1 + num2;
    }

    public static int ActorSpotMurdererChance(Actor spotter, Actor murderer)
    {
      return MURDERER_SPOTTING_BASE_CHANCE + MURDER_SPOTTING_MURDERCOUNTER_BONUS * murderer.MurdersCounter - MURDERER_SPOTTING_DISTANCE_PENALTY * Rules.GridDistance(spotter.Location, murderer.Location);
    }

    public static int ActorBiteHpRegen(Actor a, int dmg)
    {
      int num = (int) ((double) Rules.SKILL_ZEATER_REGEN_BONUS * (double) a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_EATER) * (double) dmg);
      return dmg + num;
    }

    public static int CorpseEatingInfectionTransmission(int infection)
    {
      return (int) (0.1 * (double) infection);
    }

    public static int InfectionForDamage(Actor infector, int dmg)
    {
      return (int) ((1.0 + (double) infector.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_INFECTOR) * (double) Rules.SKILL_ZINFECTOR_BONUS) * (double) dmg);
    }

    public static int InfectionEffectTriggerChance1000(int infectionPercent)
    {
      return Rules.INFECTION_EFFECT_TRIGGER_CHANCE_1000 + infectionPercent / 5;
    }

    public static float CorpseDecayPerTurn(Corpse c)
    {
      return CORPSE_DECAY_PER_TURN;
    }

    public static int CorpseZombifyChance(Corpse c, WorldTime timeNow, bool checkDelay = true)
    {
      int num1 = timeNow.TurnCounter - c.Turn;
      if (checkDelay && num1 < CORPSE_ZOMBIFY_DELAY) return 0;
      int num2 = c.DeadGuy.InfectionPercent;
      if (checkDelay) {
        int num3 = num2 >= 100 ? 1 : 100 / (1 + num2);
        if (timeNow.TurnCounter % num3 != 0)
          return 0;
      }
      float num4 = 0.0f + 1f * (float) num2 - (float) (int) ((double) num1 / (double) WorldTime.TURNS_PER_DAY);
      return Math.Max(0, Math.Min(100, !timeNow.IsNight ? (int) (num4 * CORPSE_ZOMBIFY_DAY_FACTOR) : (int) (num4 * CORPSE_ZOMBIFY_NIGHT_FACTOR)));
    }

    public int CorpseReviveChance(Actor actor, Corpse corpse)
    {
      if (!CanActorReviveCorpse(actor, corpse)) return 0;
      return corpse.FreshnessPercent / 2 + actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.MEDIC) * Rules.SKILL_MEDIC_REVIVE_BONUS;
    }

    public static int CorpseReviveHPs(Actor actor, Corpse corpse)
    {
      return 5 + actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.MEDIC);
    }

    public bool CheckTrapTriggers(ItemTrap trap, Actor a)
    {
      if (a.Model.Abilities.IsSmall && RollChance(90)) return false;
      int num = 0 + (a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.LIGHT_FEET) * Rules.SKILL_LIGHT_FEET_TRAP_BONUS + a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_LIGHT_FEET) * Rules.SKILL_ZLIGHT_FEET_TRAP_BONUS);
      return RollChance(trap.Model.TriggerChance * trap.Quantity - num);
    }

    public bool CheckTrapTriggers(ItemTrap trap, MapObject mobj)
    {
      return RollChance(trap.Model.TriggerChance * mobj.Weight);
    }

    public bool CheckTrapStepOnBreaks(ItemTrap trap, MapObject mobj = null)
    {
      int breakChance = trap.Model.BreakChance;
      if (mobj != null)
        breakChance *= mobj.Weight;
      return RollChance(breakChance);
    }

    public bool CheckTrapEscapeBreaks(ItemTrap trap, Actor a)
    {
      return RollChance(trap.Model.BreakChanceWhenEscape);
    }

    public bool CheckTrapEscape(ItemTrap trap, Actor a)
    {
      return RollChance(0 + (a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.LIGHT_FEET) * Rules.SKILL_LIGHT_FEET_TRAP_BONUS + a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_LIGHT_FEET) * Rules.SKILL_ZLIGHT_FEET_TRAP_BONUS) + (100 - trap.Model.BlockChance * trap.Quantity));
    }

    public static int ZGrabChance(Actor grabber, Actor victim)
    {
      return grabber.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_GRAB) * Rules.SKILL_ZGRAB_CHANCE;
    }
  } // end Rules class

// still want this
#if FAIL
  internal static class ext_Rules
  {
  }
#endif
}
