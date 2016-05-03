﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Actions.ActionBashDoor
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Engine.MapObjects;
using System;

namespace djack.RogueSurvivor.Engine.Actions
{
  internal class ActionBashDoor : ActorAction
  {
    private DoorWindow m_Door;

    public ActionBashDoor(Actor actor, RogueGame game, DoorWindow door)
      : base(actor, game)
    {
      if (door == null)
        throw new ArgumentNullException("door");
            m_Door = door;
    }

    public override bool IsLegal()
    {
      return m_Game.Rules.IsBashableFor(m_Actor, m_Door, out m_FailReason);
    }

    public override void Perform()
    {
            m_Game.DoBreak(m_Actor, (MapObject)m_Door);
    }
  }
}
