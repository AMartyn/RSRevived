﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Data.Corpse
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using System;
using System.Drawing;
using System.Diagnostics.Contracts;

namespace djack.RogueSurvivor.Data
{
  [Serializable]
  internal class Corpse
  {
    public readonly Actor DeadGuy;
    public readonly int Turn;
    public Point Position;
    public float HitPoints;
    public readonly int MaxHitPoints;
    public readonly float Rotation;
    public readonly float Scale; // currently not used properly
    public Actor DraggedBy;

    public bool IsDragged {
      get {
        return DraggedBy != null && !DraggedBy.IsDead;
      }
    }

    public Corpse(Actor deadGuy, float rotation, float scale=1f)
    {
	  Contract.Requires(null != deadGuy);
      DeadGuy = deadGuy;
      Turn = deadGuy.Location.Map.LocalTime.TurnCounter;
      HitPoints = (float)deadGuy.MaxHPs;
      MaxHitPoints = deadGuy.MaxHPs;
      Rotation = rotation;
      Scale = Math.Max(0.0f, Math.Min(1f, scale));
      DraggedBy = null;
    }

    public int FreshnessPercent {
      get {
        return (int) (100.0 * (double) HitPoints / (double)DeadGuy.MaxHPs);
      }
    }

    public int RotLevel {
      get {
        int num = FreshnessPercent;
        if (num < 5) return 5;
        if (num < 25) return 4;
        if (num < 50) return 3;
        if (num < 75) return 2;
        return num < 90 ? 1 : 0;
      }
    }
  }
}
