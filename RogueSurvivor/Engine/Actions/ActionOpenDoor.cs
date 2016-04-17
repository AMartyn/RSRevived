﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Actions.ActionOpenDoor
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Engine.MapObjects;
using System;

namespace djack.RogueSurvivor.Engine.Actions
{
  internal class ActionOpenDoor : ActorAction
  {
    private DoorWindow m_Door;

    public ActionOpenDoor(Actor actor, RogueGame game, DoorWindow door)
      : base(actor, game)
    {
      if (door == null)
        throw new ArgumentNullException("door");
      this.m_Door = door;
    }

    public override bool IsLegal()
    {
      return this.m_Game.Rules.IsOpenableFor(this.m_Actor, this.m_Door, out this.m_FailReason);
    }

    public override void Perform()
    {
      this.m_Game.DoOpenDoor(this.m_Actor, this.m_Door);
    }
  }
}
