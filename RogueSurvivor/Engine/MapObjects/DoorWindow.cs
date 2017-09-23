﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.MapObjects.DoorWindow
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using System;

namespace djack.RogueSurvivor.Engine.MapObjects
{
  [Serializable]
  internal class DoorWindow : StateMapObject
  {
    public const int BASE_HITPOINTS = 40;
    public const int STATE_CLOSED = 1;
    public const int STATE_OPEN = 2;
    public const int STATE_BROKEN = 3;
    private readonly string m_ClosedImageID;
    private readonly string m_OpenImageID;
    private readonly string m_BrokenImageID;
    private readonly bool m_IsWindow;
    private int m_BarricadePoints;

    public bool IsOpen { get { return State == STATE_OPEN; } }
    public bool IsClosed { get { return State == STATE_CLOSED; } }
    public bool IsBroken { get { return State == STATE_BROKEN; } }

    public override bool IsTransparent
    {
      get {
        if (m_BarricadePoints > 0) return false;
        if (State != STATE_OPEN) return base.IsTransparent;
        return FireState != MapObject.Fire.ONFIRE;
      }
    }

    public bool IsWindow { get { return m_IsWindow; } }

    public int BarricadePoints {
      get {
        return m_BarricadePoints;
      }
      private set {
        if (value > 0 && m_BarricadePoints <= 0) {
          --JumpLevel;
          IsWalkable = false;
        }
        else if (value <= 0 && m_BarricadePoints > 0)
          SetState(State);
        if (0>value) value = 0;
        if (Rules.BARRICADING_MAX < value) value = Rules.BARRICADING_MAX;
        m_BarricadePoints = value;
      }
    }

    public bool IsBarricaded { get { return m_BarricadePoints > 0; } }

    public DoorWindow(string name, string closedImageID, string openImageID, string brokenImageID, int hitPoints)
      : base(name, closedImageID, MapObject.Break.BREAKABLE, MapObject.Fire.BURNABLE, hitPoints)
    {
      m_ClosedImageID = closedImageID;
      m_OpenImageID = openImageID;
      m_BrokenImageID = brokenImageID;
      m_BarricadePoints = 0;
      _SetState(STATE_CLOSED);
      if ("window" == name) m_IsWindow = true;  // XXX arguably should be a constructor parameter
    }

    public void Barricade(int delta)
    {
      int old = BarricadePoints;
      BarricadePoints += delta;
      if ((0 < old)!=(0 < BarricadePoints)) InvalidateLOS();
    }

    private string ReasonCantBarricade()
    {
      if (!IsClosed && !IsBroken) return "not closed or broken";
      if (BarricadePoints >= Rules.BARRICADING_MAX) return "barricade limit reached";
      if (Location.Actor != null) return "someone is there";
      return "";
    }

    public bool CanBarricade(out string reason)
    {
      reason = ReasonCantBarricade();
      return string.IsNullOrEmpty(reason);
    }

    public bool CanBarricade()
    {
      return string.IsNullOrEmpty(ReasonCantBarricade());
    }

    override protected string StateToID(int x)
    {
      switch(x)
      {
      case STATE_CLOSED: return m_ClosedImageID;
      case STATE_OPEN: return m_OpenImageID;
      case STATE_BROKEN: return m_BrokenImageID;
      default: throw new ArgumentOutOfRangeException("newState unhandled");
      }
    }

    private void _SetState(int newState)
    { // cf IsTransparent
      if ((STATE_OPEN==State)!=(STATE_OPEN==newState)) InvalidateLOS();
      base.SetState(newState);
      switch(State) {
        case STATE_CLOSED:
          IsWalkable = false;
          break;
        case STATE_OPEN:
          IsWalkable = true;
          break;
        case STATE_BROKEN:
          BreakState = Break.BROKEN;
          HitPoints = 0;
          m_BarricadePoints = 0;
          IsWalkable = true;
          break;
        default:
          throw new ArgumentOutOfRangeException("newState unhandled");
      }
    }

    public override void SetState(int newState) {
      _SetState(newState);
    }
  }
}
