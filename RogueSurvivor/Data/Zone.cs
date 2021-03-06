﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Data.Zone
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using System;
using System.Collections.Generic;

using Point = Zaimoni.Data.Vector2D_short;
using Rectangle = Zaimoni.Data.Box2D_short;
using Size = Zaimoni.Data.Vector2D_short;

namespace djack.RogueSurvivor.Data
{
  // not meant to be self-contained
  [Serializable]
  internal class Zone
  {
    private readonly string m_Name = "unnamed zone";
    private Rectangle m_Bounds; // assumed to be fully in bounds of the underlying map
    private Dictionary<string, object> m_Attributes;

    public string Name { get { return m_Name; } }
    public Rectangle Bounds { get { return m_Bounds; } }

    public Zone(string name, Rectangle bounds)
    {
#if DEBUG
      if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
#endif
      m_Name = name;
      m_Bounds = bounds;
    }

    // while zone attributes have great potential, RS Alpha 9 underwhelms in its use of them.
    public bool HasGameAttribute(string key)
    {
      return m_Attributes?.ContainsKey(key) ?? false;
    }

    public void SetGameAttribute<_T_>(string key, _T_ value)
    {
#if DEBUG
      if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
#endif
      if (m_Attributes == null) m_Attributes = new Dictionary<string, object>(1);
      m_Attributes[key] = value;
    }

    public _T_ GetGameAttribute<_T_>(string key)
    {
#if DEBUG
      if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
#endif
      if (m_Attributes == null) return default;
      if (!m_Attributes.TryGetValue(key, out object obj)) return default;
      if (!(obj is _T_)) throw new InvalidOperationException("game attribute is not of requested type");
      return (_T_) obj;
    }
  }

  [Serializable]    // just in case
  internal class ZoneLoc
  {
    public readonly Map m;
    public readonly Rectangle Rect; // doesn't have to be normalized

    public ZoneLoc(Map _m, Rectangle _r)
    {
      m = _m;
      Rect = _r;
    }

    public bool Contains(in Location loc) { return m == loc.Map && Rect.Contains(loc.Position); }
    public bool ContainsExt(in Location loc) {
      if (m == loc.Map) return Rect.Contains(loc.Position);
      var test = m.Denormalize(in loc);
      return null!=test && Rect.Contains(test.Value.Position);
    }

    public void DoForEach(Action<Location> doFn) {
      Point point = new Point();
      for (point.X = Rect.Left; point.X < Rect.Right; ++point.X) {
        for (point.Y = Rect.Top; point.Y < Rect.Bottom; ++point.Y) {
          var loc = new Location(m,point);
          if (Map.Canonical(ref loc)) doFn(loc);
        }
      }
    }

    public Location Center { get { return new Location(m, Rect.Location + Rect.Size / 2); } }

    public Rectangle DistrictSpan { get {
      var ret = new Rectangle(m.District.WorldPosition,new Size(1,1));
      if (0 < District.UsesCrossDistrictView(m)) {
        int test = Rect.Left;
        int delta = m.Width;
        while (0 > test) {
          test += delta;
          ret.X -= 1;
          ret.Width += 1;
        }
        test = Rect.Right;
        while (delta <= test) {
          test -= delta;
          ret.Width += 1;
        }
        test = Rect.Top;
        delta = m.Height;
        while (0 > test) {
          test += delta;
          ret.Y -= 1;
          ret.Height += 1;
        }
        test = Rect.Bottom;
        while (delta <= test) {
          test -= delta;
          ret.Height += 1;
        }
      }
      return ret;
    } }
  }

#if CTHORPE_BROKEN_GENERICS
  internal class ZoneSpan<T> where T:IEnumerable<Point>
  {
    public readonly Map m;
    public readonly T pts; // doesn't have to be normalized

    public ZoneSpan(Map _m, T _pts)
    {
      m = _m;
      pts = _pts;
    }

    // 2020-03-15: just because T is required to be IEnumerable<Point> doesn't make IEnumerable functions available
    public bool Contains(in Location loc) { return m == loc.Map && pts.Contains(loc.Position); }
    public bool ContainsExt(in Location loc) {
      if (m == loc.Map) return pts.Contains(loc.Position);
      var test = m.Denormalize(in loc);
      return null!=test && pts.Contains(test.Value.Position);
    }

    public void DoForEach(Action<Location> doFn) {
      foreach(var pt in pts) {
        var loc = new Location(m,pt);
        if (Map.Canonical(ref loc)) doFn(loc);
      }
    }
  }
#endif
}
