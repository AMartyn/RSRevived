﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Rules
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Engine.Actions;
using djack.RogueSurvivor.Engine.Items;
using djack.RogueSurvivor.Engine.MapObjects;
using djack.RogueSurvivor.Gameplay;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics.Contracts;

namespace djack.RogueSurvivor.Engine
{
  internal class Rules
  {
    public static int INFECTION_LEVEL_1_WEAK = 10;
    public static int INFECTION_LEVEL_2_TIRED = 30;
    public static int INFECTION_LEVEL_3_VOMIT = 50;
    public static int INFECTION_LEVEL_4_BLEED = 75;
    public static int INFECTION_LEVEL_5_DEATH = 100;
    public static int INFECTION_LEVEL_1_WEAK_STA = 24;
    public static int INFECTION_LEVEL_2_TIRED_STA = 24;
    public static int INFECTION_LEVEL_2_TIRED_SLP = 90;
    public static int INFECTION_LEVEL_4_BLEED_HP = 6;
    public static int INFECTION_EFFECT_TRIGGER_CHANCE_1000 = 2;
    public static int UPGRADE_SKILLS_TO_CHOOSE_FROM = 5;
    public static int UNDEAD_UPGRADE_SKILLS_TO_CHOOSE_FROM = 2;
    public static int SKILL_AGILE_DEF_BONUS = 2;
    public static float SKILL_AWAKE_SLEEP_REGEN_BONUS = 0.2f;
    public static float SKILL_CARPENTRY_BARRICADING_BONUS = 0.2f;
    public static int SKILL_CARPENTRY_LEVEL3_BUILD_BONUS = 1;
    public static int SKILL_CHARISMATIC_TRUST_BONUS = 1;
    public static int SKILL_CHARISMATIC_TRADE_BONUS = 10;
    public static int SKILL_HARDY_HEAL_CHANCE_BONUS = 1;
    public static float SKILL_LIGHT_EATER_FOOD_BONUS = 0.2f;
    public static int SKILL_LIGHT_FEET_TRAP_BONUS = 5;
    public static int SKILL_LIGHT_SLEEPER_WAKEUP_CHANCE_BONUS = 10;
    public static float SKILL_MEDIC_BONUS = 0.2f;
    public static int SKILL_MEDIC_REVIVE_BONUS = 10;
    public static int SKILL_MEDIC_LEVEL_FOR_REVIVE_EST = 1;
    public static int SKILL_NECROLOGY_CORPSE_BONUS = 4;
    public static int SKILL_NECROLOGY_LEVEL_FOR_INFECTION = 3;
    public static int SKILL_NECROLOGY_LEVEL_FOR_RISE = 5;
    public static float SKILL_STRONG_PSYCHE_LEVEL_BONUS = 0.15f;
    public static float SKILL_STRONG_PSYCHE_ENT_BONUS = 0.15f;
    public static int SKILL_STRONG_THROW_BONUS = 1;
    public static int SKILL_UNSUSPICIOUS_BONUS = 25;
    public static int UNSUSPICIOUS_BAD_OUTFIT_PENALTY = 50;
    public static int UNSUSPICIOUS_GOOD_OUTFIT_BONUS = 50;
    public static int SKILL_ZAGILE_DEF_BONUS = 2;
    public static float SKILL_ZEATER_REGEN_BONUS = 0.2f;
    public static float SKILL_ZTRACKER_SMELL_BONUS = 0.1f;
    public static int SKILL_ZLIGHT_FEET_TRAP_BONUS = 3;
    public static int SKILL_ZGRAB_CHANCE = 2;
    public static float SKILL_ZINFECTOR_BONUS = 0.1f;
    public static float SKILL_ZLIGHT_EATER_FOOD_BONUS = 0.1f;
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
    public const int LOUD_NOISE_RADIUS = 5;
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
    public const int TRUST_TRUSTING_THRESHOLD = 12*WorldTime.TURNS_PER_HOUR;
    public const int TRUST_MIN = -12*WorldTime.TURNS_PER_HOUR;
    public const int TRUST_MAX = 2*WorldTime.TURNS_PER_DAY;
    public const int TRUST_BOND_THRESHOLD = TRUST_MAX;
    public const int TRUST_BASE_INCREASE = 1;
    public const int TRUST_GOOD_GIFT_INCREASE = 3*WorldTime.TURNS_PER_HOUR;
    public const int TRUST_MISC_GIFT_INCREASE = WorldTime.TURNS_PER_HOUR/3;
    public const int TRUST_GIVE_ITEM_ORDER_PENALTY = -WorldTime.TURNS_PER_HOUR;
    public const int TRUST_LEADER_KILL_ENEMY = 90;
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
      Contract.Requires(null != diceRoller);
      m_DiceRoller = diceRoller;
    }

    public int Roll(int min, int max)
    {
      Contract.Ensures(Contract.Result<int>()>=min);
      Contract.Ensures(Contract.Result<int>()<max);
      return m_DiceRoller.Roll(min, max);
    }

    public bool RollChance(int chance)
    {
      return m_DiceRoller.RollChance(chance);
    }

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

    public int RollDamage(int damageValue)
    {
      if (damageValue <= 0)
        return 0;
      return m_DiceRoller.Roll(damageValue / 2, damageValue + 1);
    }

    public bool CanActorPutItemIntoContainer(Actor actor, Point position)
    {
      string reason;
      return CanActorPutItemIntoContainer(actor, position, out reason);
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
      Inventory itemsAt = actor.Location.Map.GetItemsAt(actor.Location.Position);
      if (itemsAt == null || !itemsAt.Contains(it))
      {
        reason = "item not here";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorGiveItemTo(Actor actor, Actor target, Item gift, out string reason)
    {
      Contract.Requires(null != actor);
      Contract.Requires(null != target);
      Contract.Requires(null != gift);
      if (actor.IsEnemyOf(target)) {
        reason = "enemy";
        return false;
      }
      if (gift.IsEquipped) {
        reason = "equipped";
        return false;
      }
      if (target.IsSleeping) {
        reason = "sleeping";
        return false;
      }
      return target.CanGet(gift, out reason);
    }

    private static ActorAction IsBumpableFor(Actor actor, Map map, int x, int y, out string reason)
    {
      Contract.Requires(null != map);
      Contract.Requires(null != actor);
      Point point = new Point(x, y);
      reason = "";
      if (!map.IsInBounds(x, y)) {
	    return (actor.CanLeaveMap(out reason) ? new ActionLeaveMap(actor, point) : null);
      }
      ActionMoveStep actionMoveStep = new ActionMoveStep(actor, point);
      if (actionMoveStep.IsLegal()) {
        reason = "";
        return actionMoveStep;
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
        return (actor.CanChatWith(actorAt, out reason) ? new ActionChat(actor, actorAt) : null);
      }
      MapObject mapObjectAt = map.GetMapObjectAt(point);
      if (mapObjectAt != null) {
        DoorWindow door = mapObjectAt as DoorWindow;
        if (door != null) {
          if (door.IsClosed) {
            if (actor.CanOpen(door, out reason)) return new ActionOpenDoor(actor, door);
            if (actor.CanBash(door, out reason)) return new ActionBashDoor(actor, door);
            return null;
          }
          if (door.BarricadePoints > 0) {
            if (actor.CanBash(door, out reason)) return new ActionBashDoor(actor, door);
            reason = "cannot bash the barricade";
            return null;
          }
        }
        if (actor.CanGetFromContainer(point, out reason))
          return new ActionGetFromContainer(actor, point);
        if (actor.Model.Abilities.CanBashDoors && actor.CanBreak(mapObjectAt, out reason))
          return new ActionBreak(actor, mapObjectAt);
        PowerGenerator powGen = mapObjectAt as PowerGenerator;
        if (powGen != null) {
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
      string reason;
      return IsBumpableFor(actor, location, out reason);
    }

    public static ActorAction IsBumpableFor(Actor actor, Location location, out string reason)
    {
      return IsBumpableFor(actor, location.Map, location.Position.X, location.Position.Y, out reason);
    }

    public bool CanActorInitiateTradeWith(Actor speaker, Actor target)
    {
      string reason;
      return CanActorInitiateTradeWith(speaker, target, out reason);
    }

    public bool CanActorInitiateTradeWith(Actor speaker, Actor target, out string reason)
    {
      Contract.Requires(null != speaker);
      Contract.Requires(null != target);
      if (target.IsPlayer) {
        reason = "target is player";
        return false;
      }
      if (!speaker.Model.Abilities.CanTrade && target.Leader != speaker) {
        reason = "can't trade";
        return false;
      }
      if (!target.Model.Abilities.CanTrade && target.Leader != speaker) {
        reason = "target can't trade";
        return false;
      }
      if (speaker.IsEnemyOf(target)) {
        reason = "is an enemy";
        return false;
      }
      if (target.IsSleeping) {
        reason = "is sleeping";
        return false;
      }
      if (speaker.Inventory == null || speaker.Inventory.IsEmpty) {
        reason = "nothing to offer";
        return false;
      }
      if (target.Inventory == null || target.Inventory.IsEmpty) {
        reason = "has nothing to trade";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorShout(Actor speaker)
    {
      string reason;
      return CanActorShout(speaker, out reason);
    }

    public bool CanActorShout(Actor speaker, out string reason)
    {
      if (speaker == null)
        throw new ArgumentNullException("speaker");
      if (speaker.IsSleeping)
      {
        reason = "sleeping";
        return false;
      }
      if (!speaker.Model.Abilities.CanTalk)
      {
        reason = "can't talk";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorShove(Actor actor, Actor other, out string reason)
    {
      if (actor == null) throw new ArgumentNullException("actor");
      if (other == null) throw new ArgumentNullException("other");
      if (!actor.AbleToPush) {
        reason = "cannot shove people";
        return false;
      }
      if (actor.IsTired) {
        reason = "tired";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanShoveActorTo(Actor actor, Point toPos, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      Map map = actor.Location.Map;
      if (!map.IsInBounds(toPos))
      {
        reason = "out of map";
        return false;
      }
      if (!map.GetTileAt(toPos.X, toPos.Y).Model.IsWalkable) {
        reason = "blocked";
        return false;
      }
      MapObject mapObjectAt = map.GetMapObjectAt(toPos);
      if (mapObjectAt != null && !mapObjectAt.IsWalkable) {
        reason = "blocked by an object";
        return false;
      }
      if (map.GetActorAt(toPos) != null) {
        reason = "blocked by someone";
        return false;
      }
      reason = "";
      return true;
    }

    public List<Actor> GetEnemiesInFov(Actor actor, HashSet<Point> fov)
    {
      Contract.Requires(null != actor);
      Contract.Requires(null != fov);
      List<Actor> actorList = (List<Actor>) null;
      foreach (Point position in fov) {
        Actor actorAt = actor.Location.Map.GetActorAt(position);
        if (actorAt != null && actorAt != actor && actor.IsEnemyOf(actorAt)) {
          if (actorList == null)
            actorList = new List<Actor>(3);
          actorList.Add(actorAt);
        }
      }
      if (actorList != null)
        actorList.Sort((Comparison<Actor>) ((a, b) =>
        {
          float num1 = Rules.StdDistance(a.Location.Position, actor.Location.Position);
          float num2 = Rules.StdDistance(b.Location.Position, actor.Location.Position);
          if ((double) num1 < (double) num2)
            return -1;
          return (double) num1 <= (double) num2 ? 0 : 1;
        }));
      return actorList;
    }

    public bool CanActorThrowTo(Actor actor, Point pos, List<Point> LoF)
    {
      string reason;
      return CanActorThrowTo(actor, pos, LoF, out reason);
    }

    public bool CanActorThrowTo(Actor actor, Point pos, List<Point> LoF, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (LoF != null)
        LoF.Clear();
      ItemGrenade itemGrenade = actor.GetEquippedWeapon() as ItemGrenade;
      ItemGrenadePrimed itemGrenadePrimed = actor.GetEquippedWeapon() as ItemGrenadePrimed;
      if (itemGrenade == null && itemGrenadePrimed == null)
      {
        reason = "no grenade equiped";
        return false;
      }
      ItemGrenadeModel itemGrenadeModel = itemGrenade == null ? (itemGrenadePrimed.Model as ItemGrenadePrimedModel).GrenadeModel : itemGrenade.Model as ItemGrenadeModel;
      int maxRange = ActorMaxThrowRange(actor, itemGrenadeModel.MaxThrowDistance);
      if (Rules.GridDistance(actor.Location.Position, pos) > maxRange)
      {
        reason = "out of throwing range";
        return false;
      }
      if (!LOS.CanTraceThrowLine(actor.Location, pos, maxRange, LoF))
      {
        reason = "no line of throwing";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorSleep(Actor actor)
    {
      string reason;
      return CanActorSleep(actor, out reason);
    }

    public bool CanActorSleep(Actor actor, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (actor.IsSleeping)
      {
        reason = "already sleeping";
        return false;
      }
      if (!actor.Model.Abilities.HasToSleep)
      {
        reason = "no ability to sleep";
        return false;
      }
      if (actor.IsHungry || actor.IsStarving)
      {
        reason = "hungry";
        return false;
      }
      if (actor.SleepPoints >= actor.MaxSleep - WorldTime.TURNS_PER_HOUR)
      {
        reason = "not sleepy at all";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorCancelLead(Actor actor, Actor target, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (target == null)
        throw new ArgumentNullException("target");
      if (target.Leader != actor)
      {
        reason = "not your follower";
        return false;
      }
      if (target.IsSleeping)
      {
        reason = "sleeping";
        return false;
      }
      reason = "";
      return true;
    }

    public bool IsActorTrustingLeader(Actor actor)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (!actor.HasLeader)
        return false;
      return actor.TrustInLeader >= TRUST_TRUSTING_THRESHOLD;
    }

    public bool HasActorBondWith(Actor actor, Actor target)
    {
      if (actor.Leader == target)
        return actor.TrustInLeader >= TRUST_BOND_THRESHOLD;
      if (target.Leader == actor)
        return target.TrustInLeader >= TRUST_BOND_THRESHOLD;
      return false;
    }

    public int CountBarricadingMaterial(Actor actor)
    {
      if (actor.Inventory == null || actor.Inventory.IsEmpty)
        return 0;
      int num = 0;
      foreach (Item obj in actor.Inventory.Items)
      {
        if (obj is ItemBarricadeMaterial)
          num += obj.Quantity;
      }
      return num;
    }

    public bool CanActorBuildFortification(Actor actor, Point pos, bool isLarge)
    {
      string reason;
      return CanActorBuildFortification(actor, pos, isLarge, out reason);
    }

    public bool CanActorBuildFortification(Actor actor, Point pos, bool isLarge, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.CARPENTRY) == 0)
      {
        reason = "no skill in carpentry";
        return false;
      }
      Map map = actor.Location.Map;
      if (!map.GetTileAt(pos).Model.IsWalkable)
      {
        reason = "cannot build on walls";
        return false;
      }
      int num = ActorBarricadingMaterialNeedForFortification(actor, isLarge);
      if (CountBarricadingMaterial(actor) < num)
      {
        reason = string.Format("not enough barricading material, need {0}.", (object) num);
        return false;
      }
      if (map.GetMapObjectAt(pos) != null || map.GetActorAt(pos) != null)
      {
        reason = "blocked";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorRepairFortification(Actor actor, Fortification fort, out string reason)
    {
      if (actor == null)
        throw new ArgumentNullException("actor");
      if (!actor.Model.Abilities.CanUseMapObjects)
      {
        reason = "cannot use map objects";
        return false;
      }
      if (CountBarricadingMaterial(actor) <= 0)
      {
        reason = "no barricading material";
        return false;
      }
      reason = "";
      return true;
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
      string reason;
      return CanActorButcherCorpse(actor, corpse, out reason);
    }

    public bool CanActorButcherCorpse(Actor actor, Corpse corpse, out string reason)
    {
      if (actor == null) throw new ArgumentNullException("actor");
      if (corpse == null) throw new ArgumentNullException("corpse");
      if (actor.IsTired) {
        reason = "tired";
        return false;
      }
      if (corpse.Position != actor.Location.Position || !actor.Location.Map.HasCorpse(corpse)) {
        reason = "not in same location";
        return false;
      }
      reason = "";
      return true;
    }

    public bool CanActorStartDragCorpse(Actor actor, Corpse corpse)
    {
      string reason;
      return CanActorStartDragCorpse(actor, corpse, out reason);
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
      if (corpse.Position != actor.Location.Position || !actor.Location.Map.HasCorpse(corpse))
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
      string reason;
      return CanActorStopDragCorpse(actor, corpse, out reason);
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
      string reason;
      return CanActorReviveCorpse(actor, corpse, out reason);
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
      if (!actor.Inventory.HasItemMatching((Predicate<Item>) (it => it.Model.ID == GameItems.IDs.MEDICINE_MEDIKIT)))
      {
        reason = "no medikit";
        return false;
      }
      reason = "";
      return true;
    }

    public static bool IsAdjacent(Location a, Location b)
    {
      if (a.Map != b.Map)
        return false;
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

    // Euclidean plane distance
    public static float StdDistance(Point from, Point to)
    {
      int num1 = to.X - from.X;
      int num2 = to.Y - from.Y;
      return (float) Math.Sqrt((double) (num1 * num1 + num2 * num2));
    }

    public static float StdDistance(Point v)
    {
      return (float) Math.Sqrt((double) (v.X * v.X + v.Y * v.Y));
    }

    public static float LOSDistance(Point from, Point to)
    {
      int num1 = to.X - from.X;
      int num2 = to.Y - from.Y;
      return (float) Math.Sqrt(0.75 * (double) (num1 * num1 + num2 * num2));
    }

    public bool WillOtherActTwiceBefore(Actor actor, Actor other)
    {
      if (actor.IsBefore(other)) return other.ActionPoints > BASE_ACTION_COST;
      return other.ActionPoints + other.Speed > BASE_ACTION_COST;
    }

    public bool IsMurder(Actor killer, Actor victim)
    {
      if (null == killer) return false;
      if (null == victim) return false;
      if (victim.Model.Abilities.IsUndead) return false;
      if (killer.Model.Abilities.IsLawEnforcer && victim.MurdersCounter > 0) return false;
      if (killer.Faction.IsEnemyOf(victim.Faction)) return false;
      if (killer.IsSelfDefenceFrom(victim)) return false;

      // If your leader is a cop i.e. First Class Citizen, killing his enemies should not trigger murder charges.
      if (killer.HasLeader && killer.Leader.Model.Abilities.IsLawEnforcer && killer.Leader.IsEnemyOf(victim)) return false;
      if (killer.HasLeader && killer.Leader.Model.Abilities.IsLawEnforcer && victim.IsSelfDefenceFrom(killer.Leader)) return false;

      // resume old definition
      if (killer.HasLeader && killer.Leader.IsSelfDefenceFrom(victim)) return false;
      if (killer.CountFollowers > 0) {
        foreach (Actor follower in killer.Followers) {
          if (follower.IsSelfDefenceFrom(victim))
            return false;
        }
      }
      return true;
    }

    public static int ActorItemNutritionValue(Actor actor, int baseValue)
    {
      int num = (int) ((double) baseValue * (double) Rules.SKILL_LIGHT_EATER_FOOD_BONUS * (double) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.LIGHT_EATER));
      return baseValue + num;
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

    public static int ActorMaxThrowRange(Actor actor, int baseRange)
    {
      int num = Rules.SKILL_STRONG_THROW_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.STRONG);
      return baseRange + num;
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

    public static float ActorSmell(Actor actor)
    {
      return (float) (1.0 + (double) Rules.SKILL_ZTRACKER_SMELL_BONUS * (double) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_TRACKER)) * actor.Model.StartingSheet.BaseSmellRating;
    }

    public static int ActorSmellThreshold(Actor actor)
    {
      if (actor.IsSleeping) return -1;
      return (OdorScent.MAX_STRENGTH+1) - (int) ((double)ActorSmell(actor) * OdorScent.MAX_STRENGTH);
    }

    public static int ActorLoudNoiseWakeupChance(Actor actor, int noiseDistance)
    {
      return 10 + Rules.SKILL_LIGHT_SLEEPER_WAKEUP_CHANCE_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.LIGHT_SLEEPER) + Math.Max(0, (LOUD_NOISE_RADIUS - noiseDistance) * 10);
    }

    public static int ActorBarricadingMaterialNeedForFortification(Actor builder, bool isLarge)
    {
      return Math.Max(1, (isLarge ? 4 : 2) - (builder.Sheet.SkillTable.GetSkillLevel(Skills.IDs.CARPENTRY) >= 3 ? Rules.SKILL_CARPENTRY_LEVEL3_BUILD_BONUS : 0));
    }

    public static int ActorTrustIncrease(Actor actor)
    {
      return 1 + Rules.SKILL_CHARISMATIC_TRUST_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.CHARISMATIC);
    }

    public static int ActorCharismaticTradeChance(Actor actor)
    {
      return Rules.SKILL_CHARISMATIC_TRADE_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.CHARISMATIC);
    }

    public int ActorUnsuspicousChance(Actor observer, Actor actor)
    {
      int num1 = Rules.SKILL_UNSUSPICIOUS_BONUS * actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.UNSUSPICIOUS);
      int num2 = 0;
      ItemBodyArmor itemBodyArmor = actor.GetEquippedItem(DollPart.TORSO) as ItemBodyArmor;
      if (itemBodyArmor != null)
      {
        if (observer.Faction.ID == (int) GameFactions.IDs.ThePolice)
        {
          if (itemBodyArmor.IsHostileForCops())
            num2 -= Rules.UNSUSPICIOUS_BAD_OUTFIT_PENALTY;
          else if (itemBodyArmor.IsFriendlyForCops())
            num2 += Rules.UNSUSPICIOUS_GOOD_OUTFIT_BONUS;
        }
        else if (observer.Faction.ID == (int) GameFactions.IDs.TheBikers)
        {
          if (itemBodyArmor.IsHostileForBiker(observer.GangID))
            num2 -= Rules.UNSUSPICIOUS_BAD_OUTFIT_PENALTY;
          else if (itemBodyArmor.IsFriendlyForBiker(observer.GangID))
            num2 += Rules.UNSUSPICIOUS_GOOD_OUTFIT_BONUS;
        }
      }
      return num1 + num2;
    }

    public static int ActorSpotMurdererChance(Actor spotter, Actor murderer)
    {
      return MURDERER_SPOTTING_BASE_CHANCE + MURDER_SPOTTING_MURDERCOUNTER_BONUS * murderer.MurdersCounter - MURDERER_SPOTTING_DISTANCE_PENALTY * Rules.GridDistance(spotter.Location.Position, murderer.Location.Position);
    }

    public float ComputeMapPowerRatio(Map map)
    {
      int num1;
      int num2 = num1 = 0;
      foreach (MapObject mapObject in map.MapObjects)
      {
        PowerGenerator powerGenerator = mapObject as PowerGenerator;
        if (powerGenerator != null)
        {
          if (powerGenerator.IsOn)
            ++num1;
          else
            ++num2;
        }
      }
      int num3 = num1 + num2;
      if (num3 == 0)
        return 0.0f;
      return (float) num1 / (float) num3;
    }

    public int BlastDamage(int distance, BlastAttack attack)
    {
      if (distance < 0 || distance > attack.Radius)
        throw new ArgumentOutOfRangeException(string.Format("blast distance {0} out of range", (object) distance));
      return attack.Damage[distance];
    }

    public static int ActorBiteHpRegen(Actor a, int dmg)
    {
      int num = (int) ((double) Rules.SKILL_ZEATER_REGEN_BONUS * (double) a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_EATER) * (double) dmg);
      return dmg + num;
    }

    public static int ActorBiteNutritionValue(Actor actor, int baseValue)
    {
      return (int) (10.0 + (double) (Rules.SKILL_ZLIGHT_EATER_FOOD_BONUS * (float) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_LIGHT_EATER)) + (double) (Rules.SKILL_LIGHT_EATER_FOOD_BONUS * (float) actor.Sheet.SkillTable.GetSkillLevel(Skills.IDs.LIGHT_EATER))) * baseValue;
    }

    public int CorpseEeatingInfectionTransmission(int infection)
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

    public int CorpseZombifyChance(Corpse c, WorldTime timeNow, bool checkDelay = true)
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
      return RollChance(trap.TrapModel.TriggerChance * trap.Quantity - num);
    }

    public bool CheckTrapTriggers(ItemTrap trap, MapObject mobj)
    {
      return RollChance(trap.TrapModel.TriggerChance * mobj.Weight);
    }

    public bool CheckTrapStepOnBreaks(ItemTrap trap, MapObject mobj = null)
    {
      int breakChance = trap.TrapModel.BreakChance;
      if (mobj != null)
        breakChance *= mobj.Weight;
      return RollChance(breakChance);
    }

    public bool CheckTrapEscapeBreaks(ItemTrap trap, Actor a)
    {
      return RollChance(trap.TrapModel.BreakChanceWhenEscape);
    }

    public bool CheckTrapEscape(ItemTrap trap, Actor a)
    {
      return RollChance(0 + (a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.LIGHT_FEET) * Rules.SKILL_LIGHT_FEET_TRAP_BONUS + a.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_LIGHT_FEET) * Rules.SKILL_ZLIGHT_FEET_TRAP_BONUS) + (100 - trap.TrapModel.BlockChance * trap.Quantity));
    }

    public bool IsTrapCoveringMapObjectThere(Map map, Point pos)
    {
      MapObject mapObjectAt = map.GetMapObjectAt(pos);
      if (mapObjectAt == null)
        return false;
      if (mapObjectAt.IsJumpable)
        return true;
      if (mapObjectAt.IsWalkable)
        return !(mapObjectAt is DoorWindow);
      return false;
    }

    public bool IsTrapTriggeringMapObjectThere(Map map, Point pos)
    {
      MapObject mapObjectAt = map.GetMapObjectAt(pos);
      if (mapObjectAt == null || mapObjectAt.IsWalkable || mapObjectAt.IsJumpable)
        return false;
      return !(mapObjectAt is DoorWindow);
    }

    public int ZGrabChance(Actor grabber, Actor victim)
    {
      return grabber.Sheet.SkillTable.GetSkillLevel(Skills.IDs.Z_GRAB) * Rules.SKILL_ZGRAB_CHANCE;
    }
  } // end Rules class

  internal static class ext_Rules
  {
    // 8r coordinates at grid distance r
    // 0..2r: y constant r, x increment -r to r
    // 2r...4r: x constant r, y decrement r to -r
    // 4r..64: y constant -r, x decrement r to -r
    // 4r to 8r i.e. 0: x constant -r, y increment -r to r
    static Point RadarSweep(this Point origin,int radius,int i)
    {
      Contract.Requires(0 < radius);
      Contract.Requires(int.MaxValue/8 >= radius);
      Contract.Requires(int.MaxValue-radius>=origin.X);
      Contract.Requires(int.MaxValue-radius>=origin.Y);
      Contract.Requires(int.MinValue+radius<=origin.X);
      Contract.Requires(int.MinValue+radius<=origin.Y);

      // normalize i
      i %= 8*radius;
      if (0>i) i+=8*radius;

      // parentheses are to deny compiler the option to reorder to overflow
      if (2*radius>i) return new Point((i-radius)+origin.X,radius + origin.Y);
      if (4*radius>i) return new Point(radius + origin.X, (3*radius- i) + origin.Y);
      if (6*radius>i) return new Point((5*radius-i) + origin.X, -radius + origin.Y);
      /* if (8*radius>i) */ return new Point(-radius + origin.X, (i-7* radius) + origin.Y);
    }
  }
}
