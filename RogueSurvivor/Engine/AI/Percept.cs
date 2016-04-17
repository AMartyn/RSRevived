﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.AI.Percept
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using System;

namespace djack.RogueSurvivor.Engine.AI
{
  [Serializable]
  internal class Percept
  {
    private int m_Turn;
    private Location m_Location;
    private object m_Percepted;

    public int Turn
    {
      get
      {
        return this.m_Turn;
      }
      set
      {
        this.m_Turn = value;
      }
    }

    public object Percepted
    {
      get
      {
        return this.m_Percepted;
      }
    }

    public Location Location
    {
      get
      {
        return this.m_Location;
      }
      set
      {
        this.m_Location = value;
      }
    }

    public Percept(object percepted, int turn, Location location)
    {
      if (percepted == null)
        throw new ArgumentNullException("percepted");
      this.m_Percepted = percepted;
      this.m_Turn = turn;
      this.m_Location = location;
    }

    public int GetAge(int currentGameTurn)
    {
      return Math.Max(0, currentGameTurn - this.m_Turn);
    }
  }
}
