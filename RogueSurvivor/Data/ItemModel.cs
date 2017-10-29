﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Data.ItemModel
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using System;

namespace djack.RogueSurvivor.Data
{
  internal class ItemModel
  {
    public Gameplay.GameItems.IDs ID { get; set; }
    public readonly string SingleName;
    public readonly string PluralName;
    public readonly string ImageID;

    private int m_StackingLimit = 1;

    public bool IsPlural { get; set; }
    public bool IsProper { get; set; }
    public string FlavorDescription { get; set; }
    public DollPart EquipmentPart { get; set; }
    public bool DontAutoEquip { get; set; }
    public bool IsUnbreakable { get; set; }
    public bool IsUnique { get; set; }
    public bool IsForbiddenToAI { get; set; }

    public int StackingLimit
    {
      get { return m_StackingLimit; }
      set {
#if DEBUG
        if (0 >= value) throw new ArgumentOutOfRangeException(nameof(value),value, "0 >= value");
#endif
        m_StackingLimit = value;
      }
    }

    public bool IsStackable { get { return 2 <= m_StackingLimit; } }
    public bool IsEquipable { get { return EquipmentPart != DollPart.NONE; } }

    public ItemModel(string aName, string theNames, string imageID)
    {
      SingleName = aName;
      PluralName = theNames;
      ImageID = imageID;
    }
  }
}
