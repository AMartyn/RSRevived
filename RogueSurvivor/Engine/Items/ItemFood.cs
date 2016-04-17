﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Items.ItemFood
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using System;

namespace djack.RogueSurvivor.Engine.Items
{
  [Serializable]
  internal class ItemFood : Item
  {
    public int Nutrition { get; private set; }

    public bool IsPerishable { get; private set; }

    public WorldTime BestBefore { get; private set; }

    public ItemFood(ItemModel model)
      : base(model)
    {
      if (!(model is ItemFoodModel))
        throw new ArgumentException("model is not a FoodModel");
      this.Nutrition = (model as ItemFoodModel).Nutrition;
      this.IsPerishable = false;
    }

    public ItemFood(ItemModel model, int bestBefore)
      : base(model)
    {
      if (!(model is ItemFoodModel))
        throw new ArgumentException("model is not a FoodModel");
      this.Nutrition = (model as ItemFoodModel).Nutrition;
      this.BestBefore = new WorldTime(bestBefore);
      this.IsPerishable = true;
    }
  }
}
