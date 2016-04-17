﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Items.ItemMeleeWeapon
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using System;

namespace djack.RogueSurvivor.Engine.Items
{
  [Serializable]
  internal class ItemMeleeWeapon : ItemWeapon
  {
    public bool IsFragile
    {
      get
      {
        return (this.Model as ItemMeleeWeaponModel).IsFragile;
      }
    }

    public ItemMeleeWeapon(ItemModel model)
      : base(model)
    {
      if (!(model is ItemMeleeWeaponModel))
        throw new ArgumentException("model is not a MeleeWeaponModel");
    }
  }
}
