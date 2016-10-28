﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.MapObjects.Board
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using System;

namespace djack.RogueSurvivor.Engine.MapObjects
{
  [Serializable]
  internal class Board : MapObject
  {
    public string[] Text { get; private set; }

    public Board(string name, string imageID, string[] text)
      : base(name, imageID)
    {
      Text = text;
    }
  }
}
