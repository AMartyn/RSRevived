﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Data.Skill
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using System;

namespace djack.RogueSurvivor.Data
{
  [Serializable]
  internal class Skill
  {
    private int m_ID;
    private int m_Level;

    public int ID
    {
      get
      {
        return this.m_ID;
      }
    }

    public int Level
    {
      get
      {
        return this.m_Level;
      }
      set
      {
        this.m_Level = value;
      }
    }

    public Skill(int id)
    {
      this.m_ID = id;
    }
  }
}
