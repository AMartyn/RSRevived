﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Items.ItemExplosiveModel
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;

#nullable enable

namespace djack.RogueSurvivor.Engine.Items
{
  internal class ItemExplosiveModel : ItemModel
  {
    public readonly int FuseDelay;
    private readonly BlastAttack m_Attack;
    public readonly string BlastImage;

    public BlastAttack BlastAttack { get { return m_Attack; } } // need value copy here

    public ItemExplosiveModel(Gameplay.GameItems.IDs _id, string aName, string theNames, string imageID, int fuseDelay, BlastAttack attack, string blastImageID, string flavor)
    : base(_id, aName, theNames, imageID, flavor, DollPart.RIGHT_HAND)
    {
      FuseDelay = fuseDelay;
      m_Attack = attack;
      BlastImage = blastImageID;
    }
  }
}
