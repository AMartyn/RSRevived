﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Actions.ActionUseExit
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using System.Drawing;

namespace djack.RogueSurvivor.Engine.Actions
{
  internal class ActionUseExit : ActorAction
  {
    private Point m_ExitPoint;

    public ActionUseExit(Actor actor, Point exitPoint, RogueGame game)
      : base(actor, game)
    {
      this.m_ExitPoint = exitPoint;
    }

    public override bool IsLegal()
    {
      return this.m_Game.Rules.CanActorUseExit(this.m_Actor, this.m_ExitPoint, out this.m_FailReason);
    }

    public override void Perform()
    {
      this.m_Game.DoUseExit(this.m_Actor, this.m_ExitPoint);
    }
  }
}
