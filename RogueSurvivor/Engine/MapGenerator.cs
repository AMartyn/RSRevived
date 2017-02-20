﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.MapGenerator
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

#define TRACKING_FLOODFILL

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Engine.MapObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics.Contracts;

namespace djack.RogueSurvivor.Engine
{
  internal abstract class MapGenerator
  {
    protected readonly Rules m_Rules;

    public MapGenerator(Rules rules)
    {
	  Contract.Requires(null != rules);
      m_Rules = rules;
    }

    public abstract Map Generate(int seed, string name);

#region Tile filling
    public void TileFill(Map map, TileModel model, bool inside)
    {
      TileFill(map, model, 0, 0, map.Width, map.Height, inside);
    }

    public void TileFill(Map map, TileModel model, Action<Tile, TileModel, int, int> decoratorFn=null)
    {
      TileFill(map, model, 0, 0, map.Width, map.Height, decoratorFn);
    }

    public void TileFill(Map map, TileModel model, Rectangle rect, bool inside)
    {
      TileFill(map, model, rect.Left, rect.Top, rect.Width, rect.Height, inside);
    }

    public void TileFill(Map map, TileModel model, Rectangle rect, Action<Tile, TileModel, int, int> decoratorFn=null)
    {
      TileFill(map, model, rect.Left, rect.Top, rect.Width, rect.Height, decoratorFn);
    }

    public void TileFill(Map map, TileModel model, int left, int top, int width, int height, Action<Tile, TileModel, int, int> decoratorFn=null)
    {
      if (map == null) throw new ArgumentNullException("map");
      if (model == null) throw new ArgumentNullException("model");
      for (int x = left; x < left + width; ++x) {
        for (int y = top; y < top + height; ++y) {
          TileModel model1 = map.GetTileModelAt(x, y);
          map.SetTileModelAt(x, y, model);
          if (decoratorFn != null)
            decoratorFn(map.GetTileAt(x, y), model1, x, y);
        }
      }
    }

    public void TileFill(Map map, TileModel model, int left, int top, int width, int height, bool inside)
    {
      if (map == null) throw new ArgumentNullException("map");
      if (model == null) throw new ArgumentNullException("model");
      for (int x = left; x < left + width; ++x) {
        for (int y = top; y < top + height; ++y) {
          map.SetTileModelAt(x, y, model);
          map.SetIsInsideAt(x, y, inside);
        }
      }
    }

    public void TileHLine(Map map, TileModel model, int left, int top, int width, Action<Tile, TileModel, int, int> decoratorFn=null)
    {
      if (map == null) throw new ArgumentNullException("map");
      if (model == null) throw new ArgumentNullException("model");
      for (int x = left; x < left + width; ++x) {
        TileModel model1 = map.GetTileModelAt(x, top);
        map.SetTileModelAt(x, top, model);
        if (decoratorFn != null)
          decoratorFn(map.GetTileAt(x, top), model1, x, top);
      }
    }

    public void TileVLine(Map map, TileModel model, int left, int top, int height, Action<Tile, TileModel, int, int> decoratorFn=null)
    {
      if (map == null) throw new ArgumentNullException("map");
      if (model == null) throw new ArgumentNullException("model");
      for (int y = top; y < top + height; ++y) {
        TileModel model1 = map.GetTileModelAt(left, y);
        map.SetTileModelAt(left, y, model);
        if (decoratorFn != null)
          decoratorFn(map.GetTileAt(left, y), model1, left, y);
      }
    }

    public void TileRectangle(Map map, TileModel model, Rectangle rect)
    {
      TileRectangle(map, model, rect.Left, rect.Top, rect.Width, rect.Height);
    }

    public void TileRectangle(Map map, TileModel model, int left, int top, int width, int height, Action<Tile, TileModel, int, int> decoratorFn=null)
    {
      if (map == null) throw new ArgumentNullException("map");
      if (model == null) throw new ArgumentNullException("model");
      TileHLine(map, model, left, top, width, decoratorFn);
      TileHLine(map, model, left, top + height - 1, width, decoratorFn);
      TileVLine(map, model, left, top, height, decoratorFn);
      TileVLine(map, model, left + width - 1, top, height, decoratorFn);
    }

    public Point DigUntil(Map map, TileModel model, Point startPos, Direction digDirection, Predicate<Point> stopFn)
    {
      Point p = startPos + digDirection;
      while (map.IsInBounds(p) && !stopFn(p))
      {
        map.SetTileModelAt(p.X, p.Y, model);
        p += digDirection;
      }
      return p;
    }

    public void DoForEachTile(Rectangle rect, Action<Point> doFn)
    {
      if (doFn == null) throw new ArgumentNullException("doFn");
      Point point = new Point();
      for (point.X = rect.Left; point.X < rect.Right; ++point.X) {
        for (point.Y = rect.Top; point.Y < rect.Bottom; ++point.Y) {
          doFn(point);
        }
      }
    }

    public bool CheckForEachTile(Rectangle rect, Predicate<Point> predFn)
    {
      if (predFn == null) throw new ArgumentNullException("predFn");
      Point point = new Point();
      for (point.X = rect.Left; point.X < rect.Right; ++point.X) {
        for (point.Y = rect.Top; point.Y < rect.Bottom; ++point.Y) {
          if (!predFn(point)) return false;
        }
      }
      return true;
    }
#endregion

#region Placing actors
    public bool ActorPlace(DiceRoller roller, int maxTries, Map map, Actor actor)
    {
      return ActorPlace(roller, maxTries, map, actor, (Predicate<Point>) null);
    }

    public bool ActorPlace(DiceRoller roller, int maxTries, Map map, Actor actor, int left, int top, int width, int height)
    {
      return ActorPlace(roller, maxTries, map, actor, left, top, width, height, (Predicate<Point>) null);
    }

    public bool ActorPlace(DiceRoller roller, int maxTries, Map map, Actor actor, Predicate<Point> goodPositionFn)
    {
      return ActorPlace(roller, maxTries, map, actor, 0, 0, map.Width, map.Height, goodPositionFn);
    }

    // Las Vegas algorithm for efficiency reasons.  A temporary array of 10,000 int is unreasonable.
    public bool ActorPlace(DiceRoller roller, int maxTries, Map map, Actor actor, int left, int top, int width, int height, Predicate<Point> goodPositionFn)
    {
      Contract.Requires(null != map);
      Contract.Requires(null != actor);
      Point position = new Point();
#if TRACKING_FLOODFILL
      List<Point> valid_spawn = new List<Point>();
	  if (actor.Faction.IsEnemyOf(Models.Factions[(int)Gameplay.GameFactions.IDs.ThePolice])) {
	    for (int x=left; x<left+width; ++x) {
	      position.X = x;
	      for (int y=top; y<top+height; ++y) {
		    position.Y = y;
            if (map.IsWalkableFor(position, actor) && (goodPositionFn == null || goodPositionFn(position))) {
		      valid_spawn.Add(position);
	        }
		  }
	    }
	  }
#endif

      for (int index = 0; index < maxTries; ++index) {
        position.X = roller.Roll(left, left + width);
        position.Y = roller.Roll(top, top + height);
        if (map.IsWalkableFor(position, actor) && (goodPositionFn == null || goodPositionFn(position))) {
          map.PlaceActorAt(actor, position);
#if TRACKING_FLOODFILL
		  if (0<valid_spawn.Count) Session.Get.PoliceThreatTracking.RecordSpawn(actor, map, valid_spawn);
#endif
          return true;
        }
      }
      return false;
    }
#endregion

#region Map Objects
    public void MapObjectPlace(Map map, int x, int y, MapObject mapObj)
    {
      if (map.GetMapObjectAt(x, y) != null) return;
      map.PlaceMapObjectAt(mapObj, new Point(x, y));
    }

    public void MapObjectFill(Map map, Rectangle rect, Func<Point, MapObject> createFn)
    {
      MapObjectFill(map, rect.Left, rect.Top, rect.Width, rect.Height, createFn);
    }

    // V0.10.0
    // While createFn is likely to be an expensive function, it is also likely to use the RNG
    // that is, rearranging this function for efficiency will change level generation
    public void MapObjectFill(Map map, int left, int top, int width, int height, Func<Point, MapObject> createFn)
    {
      Point point = new Point();
      for (int x = left; x < left + width; ++x) {
        point.X = x;
        for (int y = top; y < top + height; ++y) {
          point.Y = y;
          MapObject mapObj = createFn(point);
          if (mapObj != null && map.GetMapObjectAt(x, y) == null)
            map.PlaceMapObjectAt(mapObj, point);
        }
      }
    }

    public void MapObjectPlaceInGoodPosition(Map map, Rectangle rect, Func<Point, bool> isGoodPosFn, DiceRoller roller, Func<Point, MapObject> createFn)
    {
      MapObjectPlaceInGoodPosition(map, rect.Left, rect.Top, rect.Width, rect.Height, isGoodPosFn, roller, createFn);
    }

    public void MapObjectPlaceInGoodPosition(Map map, int left, int top, int width, int height, Func<Point, bool> isGoodPosFn, DiceRoller roller, Func<Point, MapObject> createFn)
    {
      List<Point> pointList = (List<Point>) null;
      Point point = new Point();
      for (int x = left; x < left + width; ++x) {
        point.X = x;
        for (int y = top; y < top + height; ++y) {
          point.Y = y;
          if (isGoodPosFn(point) && map.GetMapObjectAt(x, y) == null) {
            (pointList ?? (pointList = new List<Point>())).Add(point);
          }
        }
      }
      if (pointList == null) return;
      int index = roller.Roll(0, pointList.Count);
      MapObject mapObj = createFn(pointList[index]);
      if (mapObj == null) return;
      map.PlaceMapObjectAt(mapObj, pointList[index]);
    }
#endregion

    public void ItemsDrop(Map map, Rectangle rect, Func<Point, bool> isGoodPositionFn, Func<Point, Item> createFn)
    {
      Point position = new Point();
      for (int left = rect.Left; left < rect.Left + rect.Width; ++left) {
        position.X = left;
        for (int top = rect.Top; top < rect.Top + rect.Height; ++top) {
          position.Y = top;
          if (isGoodPositionFn(position)) {
            Item it = createFn(position);
            if (it != null) map.DropItemAt(it, position);
          }
        }
      }
    }

    protected void ClearRectangle(Map map, Rectangle rect)
    {
      for (int left = rect.Left; left < rect.Right; ++left) {
        for (int top = rect.Top; top < rect.Bottom; ++top) {
          map.RemoveMapObjectAt(left, top);
          Inventory itemsAt = map.GetItemsAt(left, top);
          if (itemsAt != null) {
            while (!itemsAt.IsEmpty)
              map.RemoveItemAt(itemsAt[0], left, top);
          }
          map.RemoveAllDecorationsAt(left, top);
          map.RemoveAllZonesAt(left, top);
          Actor actorAt = map.GetActorAt(left, top);
          if (actorAt != null) map.RemoveActor(actorAt);
        }
      }
    }

#region Predicates and Actions
    public int CountAdjWalls(Map map, int x, int y)
    {
      return map.CountAdjacentTo(x, y, pt => !map.GetTileModelAt(pt).IsWalkable);
    }

    public int CountAdjWalls(Map map, Point p)
    {
      return CountAdjWalls(map, p.X, p.Y);
    }

    public int CountAdjWalkables(Map map, int x, int y)
    {
      return map.CountAdjacentTo(x, y, pt => map.GetTileModelAt(pt).IsWalkable);
    }

    public int CountAdjDoors(Map map, int x, int y)
    {
      return map.CountAdjacentTo(x, y, pt => map.GetMapObjectAt(pt.X, pt.Y) is DoorWindow);
    }

    public void PlaceIf(Map map, int x, int y, TileModel floor, Func<int, int, bool> predicateFn, Func<int, int, MapObject> createFn)
    {
      Contract.Requires(null != predicateFn);
      Contract.Requires(null != createFn);
      MapObject mapObj = createFn(x, y);
      if (mapObj == null) return;
      map.SetTileModelAt(x, y, floor);  // XXX wasn't this already done by the caller?
      MapObjectPlace(map, x, y, mapObj);
    }

    public bool IsAccessible(Map map, int x, int y)
    {
      return map.CountAdjacentTo(x, y, pt => map.IsWalkable(pt.X, pt.Y)) >= 6;
    }

    public bool HasNoObjectAt(Map map, int x, int y)
    {
      return map.GetMapObjectAt(x, y) == null;
    }

    public bool IsInside(Map map, int x, int y)
    {
      return map.GetTileAt(x, y).IsInside;
    }

    public bool HasInRange(Map map, Point from, int maxDistance, Predicate<Point> predFn)
    {
      int x1 = from.X - maxDistance;
      int y1 = from.Y - maxDistance;
      int x2 = from.X + maxDistance;
      int y2 = from.Y + maxDistance;
      map.TrimToBounds(ref x1, ref y1);
      map.TrimToBounds(ref x2, ref y2);
      Point point = new Point();
      for (int index1 = x1; index1 <= x2; ++index1)
      {
        point.X = index1;
        for (int index2 = y1; index2 <= y2; ++index2)
        {
          point.Y = index2;
          if ((index1 != from.X || index2 != from.Y) && predFn(point))
            return true;
        }
      }
      return false;
    }
#endregion
  }
}
