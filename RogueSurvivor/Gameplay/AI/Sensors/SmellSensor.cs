﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Gameplay.AI.Sensors.SmellSensor
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Engine;
using djack.RogueSurvivor.Engine.AI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics.Contracts;
using Zaimoni.Data;

using Percept = djack.RogueSurvivor.Engine.AI.Percept_<object>;

namespace djack.RogueSurvivor.Gameplay.AI.Sensors
{
  [Serializable]
  internal class SmellSensor : Sensor
  {
    private readonly Odor m_OdorToSmell;
    private readonly List<Percept> m_List = new List<Percept>(9);

    public List<Percept> Scents { get { return m_List; } }

    public SmellSensor(Odor odorToSmell)
    {
      m_OdorToSmell = odorToSmell;
    }

    public List<Percept> Sense(Actor actor)
    {
      Contract.Requires(1 <= actor.SmellThreshold);
      m_List.Clear();
      int num = actor.SmellThreshold;  // floors at 1
      Rectangle survey = new Rectangle(actor.Location.Position.X - 1, actor.Location.Position.Y - 1, 3, 3);
      Map map = actor.Location.Map;
      map.TrimToBounds(ref survey);
      int turnCounter = actor.Location.Map.LocalTime.TurnCounter;
      int scentByOdorAt = 0;
      survey.DoForEach(pt => { 
        m_List.Add(new Percept(new AIScent(m_OdorToSmell, scentByOdorAt), turnCounter, new Location(map, pt)));
      },pt => { 
        scentByOdorAt = map.GetScentByOdorAt(m_OdorToSmell, pt); // XXX 0 is the no-scent value
        return scentByOdorAt >= num;
      });
      return m_List;
    }

    [Serializable]
    public class AIScent
    {
      public readonly Odor Odor;
      public readonly int Strength;

      public AIScent(Odor odor, int strength)
      {
        Odor = odor;
        Strength = strength;
      }
    }
  }
}
