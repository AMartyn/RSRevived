﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Data.PlayerController
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Engine;
using djack.RogueSurvivor.Gameplay.AI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Percept = djack.RogueSurvivor.Engine.AI.Percept_<object>;
using ItemLight = djack.RogueSurvivor.Engine.Items.ItemLight;
using ItemMedicine = djack.RogueSurvivor.Engine.Items.ItemMedicine;
using ItemTracker = djack.RogueSurvivor.Engine.Items.ItemTracker;

namespace djack.RogueSurvivor.Data
{
  [Serializable]
  internal class PlayerController : ObjectiveAI
    {
    private Gameplay.AI.Sensors.LOSSensor m_LOSSensor;
    private Zaimoni.Data.Ary2Dictionary<Location, Gameplay.GameItems.IDs, int> m_itemMemory;
    private readonly List<Data.Message> m_MsgCache = new List<Data.Message>();

	public PlayerController() {
      // XXX filter should be by the normal filter type of the AI being substituted for
      m_LOSSensor = new Gameplay.AI.Sensors.LOSSensor(Gameplay.AI.Sensors.LOSSensor.SensingFilter.ACTORS | Gameplay.AI.Sensors.LOSSensor.SensingFilter.ITEMS | Gameplay.AI.Sensors.LOSSensor.SensingFilter.CORPSES);
      m_itemMemory = new Zaimoni.Data.Ary2Dictionary<Location, Gameplay.GameItems.IDs, int>();
    }

    public void DeferMessage(Data.Message x) { m_MsgCache.Add(x); }
    public void DeferMessages(IEnumerable<Data.Message> x) {
      foreach(Data.Message msg in x) m_MsgCache.Add(msg);
    }
    public List<Data.Message> ReleaseMessages() {
      if (0 >= m_MsgCache.Count) return null;
      var ret = new List<Data.Message>(m_MsgCache);
      m_MsgCache.Clear();
      return ret;
    }

    public override Zaimoni.Data.Ary2Dictionary<Location, Gameplay.GameItems.IDs, int> ItemMemory { get { return m_itemMemory; } }

	private Gameplay.AI.Sensors.LOSSensor.SensingFilter VISION_SEES() {
	  switch(m_Actor.Model.DefaultController.Name)
	  {
	  case nameof(CHARGuardAI): return Gameplay.AI.CHARGuardAI.VISION_SEES;
	  case nameof(CivilianAI): return Gameplay.AI.CivilianAI.VISION_SEES;
	  case nameof(FeralDogAI): return Gameplay.AI.FeralDogAI.VISION_SEES;
	  case nameof(GangAI): return Gameplay.AI.GangAI.VISION_SEES;
	  case nameof(InsaneHumanAI): return Gameplay.AI.InsaneHumanAI.VISION_SEES;
	  case nameof(RatAI): return Gameplay.AI.RatAI.VISION_SEES;
	  case nameof(SewersThingAI): return Gameplay.AI.SewersThingAI.VISION_SEES;
	  case nameof(SkeletonAI): return Gameplay.AI.SkeletonAI.VISION_SEES;
	  case nameof(SoldierAI): return Gameplay.AI.SoldierAI.VISION_SEES;
	  case nameof(ZombieAI): return Gameplay.AI.ZombieAI.VISION_SEES;
#if DEBUG
	  default: throw new InvalidOperationException("unhandled case");
#else
	  default: return Gameplay.AI.Sensors.LOSSensor.SensingFilter.ACTORS | Gameplay.AI.Sensors.LOSSensor.SensingFilter.ITEMS | Gameplay.AI.Sensors.LOSSensor.SensingFilter.CORPSES;
#endif
      }
	}

    public void InstallHandlers()
    {
      Actor.Says += HandleSay;
    }

	public override void TakeControl(Actor actor)
    {
      base.TakeControl(actor);
      Actor.Says += HandleSay;
      if ((int)Gameplay.GameFactions.IDs.ThePolice == actor.Faction.ID) {
        // use police item memory rather than ours
        m_itemMemory = Session.Get.PoliceItemMemory;
      }
	  // deal with vision capabilities
      m_LOSSensor = new Gameplay.AI.Sensors.LOSSensor(VISION_SEES());
      SensorsOwnedBy(actor);
    }

    public override void LeaveControl()
    {
      base.LeaveControl();
      Actor.Says -= HandleSay;
    }

    public override List<Percept> UpdateSensors()
    {
      return m_LOSSensor.Sense(m_Actor);
    }

    public override HashSet<Point> FOV { get { return m_LOSSensor.FOV; } }
    public override Dictionary<Point,Actor> friends_in_FOV { get { return m_LOSSensor.friends; } }
    public override Dictionary<Point,Actor> enemies_in_FOV { get { return m_LOSSensor.enemies; } }
    protected override void SensorsOwnedBy(Actor actor) { m_LOSSensor.OwnedBy(actor); }

    public override ActorAction GetAction(RogueGame game)
    {
      throw new InvalidOperationException("do not call PlayerController.GetAction()");
    }

    protected override ActorAction SelectAction(RogueGame game)
    {
      throw new InvalidOperationException("do not call PlayerController.SelectAction()");
    }

    public bool AutoPilotIsOn { get { return 0 > Objectives.Count;  } }

    // This is too dangerous to provide a member function for in ObjectiveAI.
    // We duplicate this code fragment from CivilianAI::SelectAction and siblings to support a reasonable replacement 
    // for the wait command (which has been removed as a cause of 1-keystroke deaths)
    public ActorAction AutoPilot()
    {
      if (0 >= Objectives.Count) return null;
      ActorAction goal_action = null;
      foreach(var o in Objectives.ToList()) {
        if (o.IsExpired) Objectives.Remove(o);
        else if (o.UrgentAction(out goal_action)) {
          if (null==goal_action) Objectives.Remove(o);
#if DEBUG
          else if (!goal_action.IsLegal()) throw new InvalidOperationException("result of UrgentAction should be legal");
#else
          else if (!goal_action.IsLegal()) Objectives.Remove(o);
#endif
          else return goal_action;
        }
      }
      return null;
    }

    public List<string> GetValidSelfOrders()
    { 
      var ret = new List<string>();
      bool in_combat = (0 < (m_Actor.Controller.enemies_in_FOV?.Count ?? 0));

      if (!in_combat) {
      if (m_Actor.IsTired && null == enemies_in_FOV) ret.Add("Rest in place");

      ItemMedicine stim = (m_Actor?.Inventory.GetBestDestackable(Models.Items[(int)Gameplay.GameItems.IDs.MEDICINE_PILLS_STA]) as ItemMedicine);
      if (null != stim) {
        MapObject car = null;
        foreach(Direction dir in Direction.COMPASS) {
          Point pt = m_Actor.Location.Position + dir;
          MapObject tmp = m_Actor.Location.Map.GetMapObjectAtExt(pt);
          if (null == tmp) continue;
          switch(tmp.ID) {
          case MapObject.IDs.CAR1:
          case MapObject.IDs.CAR2:
          case MapObject.IDs.CAR3:
          case MapObject.IDs.CAR4:
            car = tmp;
            break;
          default: continue;
          }
          break;
        }
        if (null != car) {
          int threshold = m_Actor.MaxSTA-(Rules.ActorMedicineEffect(m_Actor, stim.StaminaBoost))+2;
          if (Actor.STAMINA_MIN_FOR_ACTIVITY+MapObject.CAR_WEIGHT < threshold) threshold = Actor.STAMINA_MIN_FOR_ACTIVITY + MapObject.CAR_WEIGHT;   // no-op at 30 turns/hour, but not at 900 turns/hour
          if (m_Actor.StaminaPoints < threshold && null == enemies_in_FOV) ret.Add("Brace for pushing car in place");
        }
      }

      var generators = m_Actor.Location.Map.PowerGenerators.Get.Where(power => Rules.IsAdjacent(m_Actor.Location,power.Location)).ToList();
      if (0 < generators.Count) {
        var lights = m_Actor?.Inventory.GetItemsByType<ItemLight>(it => it.MaxBatteries-1>it.Batteries);
        var trackers = m_Actor?.Inventory.GetItemsByType<ItemTracker>(it => Gameplay.GameItems.IDs.TRACKER_POLICE_RADIO != it.Model.ID && it.MaxBatteries - 1 > it.Batteries);
        if (0 < (lights?.Count ?? 0) || 0 < (trackers?.Count ?? 0)) ret.Add("Recharge everything to full");
      }

      if (m_Actor.IsTired) ret.Add("Rest rather than lose turn when tired");

      Objective test = new Goal_MedicateSLP(Session.Get.WorldTime.TurnCounter, m_Actor);
      ActorAction testAction = null;
      if (test.UrgentAction(out testAction) && null!=testAction) ret.Add("Medicate sleep");
      } // if (!in_combat)
      return ret;
    }

    public bool InterpretSelfOrder(int i, List<string> orders)
    {
      switch(orders[i])
      {
      case "Rest in place":
        Objectives.Insert(0,new Goal_RecoverSTA(Session.Get.WorldTime.TurnCounter,m_Actor,Actor.STAMINA_MIN_FOR_ACTIVITY));
        return true;
      case "Brace for pushing car in place":
        {
        ItemMedicine stim = (m_Actor?.Inventory.GetBestDestackable(Models.Items[(int)Gameplay.GameItems.IDs.MEDICINE_PILLS_STA]) as ItemMedicine);
        if (null == stim) return false; // actually invariant failure
        int threshold = m_Actor.MaxSTA-(Rules.ActorMedicineEffect(m_Actor, stim.StaminaBoost))+2;
        // currently all wrecked cars have weight 100
        if (Actor.STAMINA_MIN_FOR_ACTIVITY+MapObject.CAR_WEIGHT < threshold) threshold = Actor.STAMINA_MIN_FOR_ACTIVITY + MapObject.CAR_WEIGHT;   // no-op at 30 turns/hour, but not at 900 turns/hour
        Objectives.Insert(0,new Goal_RecoverSTA(Session.Get.WorldTime.TurnCounter,m_Actor, threshold));
        }
        return true;
      case "Recharge everything to full":
        Objectives.Insert(0,new Goal_RechargeAll(Session.Get.WorldTime.TurnCounter, m_Actor));
        return true;
      case "Rest rather than lose turn when tired":
        Objectives.Insert(0,new Goal_RestRatherThanLoseturnWhenTired(Session.Get.WorldTime.TurnCounter, m_Actor));
        return true;
      case "Medicate sleep":
        Objectives.Insert(0,new Goal_MedicateSLP(Session.Get.WorldTime.TurnCounter, m_Actor));
        return true;
      default: return false;  // automatic failure
      }
    }

    public override bool IsInterestingTradeItem(Actor speaker, Item offeredItem)
    {
      return true;
    }

    // while the following is "valid" for any actor, messages are shown *only* to the player
    public Data.Message MakeCentricMessage(string eventText, Location loc, Color? color=null)
    {
      Location? test = m_Actor.Location.Map.Denormalize(loc);
      if (null == test) throw new ArgumentNullException(nameof(test));
      Point v = new Point(test.Value.Position.X - m_Actor.Location.Position.X, test.Value.Position.Y - m_Actor.Location.Position.Y);
      string msg_text = string.Format("{0} {1} tiles to the {2}.", eventText, (int)Rules.StdDistance(v), Direction.ApproximateFromVector(v));
      if (null != color) return new Data.Message(msg_text, Session.Get.WorldTime.TurnCounter, color.Value);
      return new Data.Message(msg_text, Session.Get.WorldTime.TurnCounter);
    }

    private void HandleSay(object sender, Actor.SayArgs e)
    {
      Actor speaker = (sender as Actor);
      if (null == speaker) throw new ArgumentNullException(nameof(sender));
      if (null == e._target) throw new ArgumentNullException("e.target");
      lock (speaker) {
        if (e.shown) return;
        if (m_Actor.IsSleeping) return;
        if (!CanSee(speaker.Location) && !CanSee(e._target.Location)) return;
        if (m_Actor!= e._target && e._target.IsPlayer) return;
        e.shown = true;
      }
      RogueForm.Game.PanViewportTo(m_Actor);

      if (e._important) RogueForm.Game.ClearMessages();
      foreach(Data.Message tmp in e.messages) {
        RogueForm.Game.AddMessage(tmp);
      }
      if (!e._important) return;

      RogueForm.Game.AddOverlay(new RogueGame.OverlayRect(Color.Yellow, new Rectangle(RogueGame.MapToScreen(speaker.Location), RogueGame.SIZE_OF_ACTOR)));
      RogueForm.Game.AddMessagePressEnter();
      RogueForm.Game.ClearOverlays();
//    RogueForm.Game.RemoveLastMessage();
      RogueForm.Game.RedrawPlayScreen();
    }
  }
}
