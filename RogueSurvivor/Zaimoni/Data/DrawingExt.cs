﻿using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace Zaimoni.Data
{
  public static class ext_Drawing
  {
    // 8r coordinates at grid distance r
    // 0..2r: y constant r, x increment -r to r
    // 2r...4r: x constant r, y decrement r to -r
    // 4r..64: y constant -r, x decrement r to -r
    // 4r to 8r i.e. 0: x constant -r, y increment -r to r
    public static Point RadarSweep(this Point origin,int radius,int i)
    {
      Contract.Requires(0 < radius);
      Contract.Requires(int.MaxValue/8 >= radius);
      Contract.Requires(int.MaxValue-radius>=origin.X);
      Contract.Requires(int.MaxValue-radius>=origin.Y);
      Contract.Requires(int.MinValue+radius<=origin.X);
      Contract.Requires(int.MinValue+radius<=origin.Y);

      // normalize i
      i %= 8*radius;
      if (0>i) i+=8*radius;

      // parentheses are to deny compiler the option to reorder to overflow
      if (2*radius>i) return new Point((i-radius)+origin.X,radius + origin.Y);
      if (4*radius>i) return new Point(radius + origin.X, (3*radius- i) + origin.Y);
      if (6*radius>i) return new Point((5*radius-i) + origin.X, -radius + origin.Y);
      /* if (8*radius>i) */ return new Point(-radius + origin.X, (i-7* radius) + origin.Y);
    }

    // low-level bitmap manipulations.  Technically redundant due to System.Drawing.Graphics.
    public static Bitmap MonochromeRectangle(Color tint, int w, int h)
    {
      Bitmap img = new Bitmap(w,h);
      for (int x = 0; x < w; ++x) {
        for (int y = 0; y < h; ++y) {
          img.SetPixel(x,y,tint);
        }
      }
      return img;
    }

    // y and x1 aware of negative offsets being from the other end
    // half-open interval [x0,x1[
    public static void HLine(this Bitmap img, Color tint, int y, int x0, int x1)
    {
      if (img.Height < y) return;
      if (0 > y) {
        y += img.Height;
        if (0 > y) return;
      }
      if (0 > x1) {
        x1 += img.Width+1;
        if (0 > x1) return;
      } else if (img.Width < x1) x1 = img.Width;
      if (0 > x0) x0 = 0;
      while(x0<x1) {
        img.SetPixel(x0++, y, tint);
      }
    }

    // x and y1 aware of negative offsets being from the other end
    // half-open interval [y0,y1[
    public static void VLine(this Bitmap img, Color tint, int x, int y0, int y1)
    {
      if (img.Width < x) return;
      if (0 > x) {
        x += img.Width;
        if (0 > x) return;
      }
      if (0 > y1) {
        y1 += img.Height+1;
        if (0 > y1) return;
      } else if (img.Height < y1) y1 = img.Height;
      if (0 > y0) y0 = 0;
      while(y0 < y1) {
        img.SetPixel(x, y0++, tint);
      }
    }

    // Following might actually be redundant due to System.Linq, but a dictionary i.e. associative array really is two sequences (keys and values)
    public static Dictionary<Key, Value> OnlyIf<Key,Value>(this Dictionary<Key,Value> src,Predicate<Value> fn)
    {
      foreach(Key k in src.Keys.ToList()) {
        if (!fn(src[k])) src.Remove(k);
      }
      return src;
    }

    public static Dictionary<Key, Value> OnlyIf<Key,Value>(this Dictionary<Key,Value> src,Predicate<Key> fn)
    {
      foreach(Key k in src.Keys.ToList()) {
        if (!fn(k)) src.Remove(k);
      }
      return src;
    }

    public static T Minimize<T,R>(this IEnumerable<T> src,Func<T,R> metric) where R:IComparable
    {
      R num1 = (R)typeof(R).GetField("MaxValue").GetValue(default(R));
      T ret = default(T);
      foreach(T test in src) {
         R num2 = metric(test);
         if (0>num2.CompareTo(num1)) {
           ret = test;
           num1 = num2;
         }
      }
      return ret;
    }

    public static T Maximize<T,R>(this IEnumerable<T> src,Func<T,R> metric) where R:IComparable
    {
      R num1 = (R)typeof(R).GetField("MinValue").GetValue(default(R));
      T ret = default(T);
      foreach(T test in src) {
         R num2 = metric(test);
         if (0<num2.CompareTo(num1)) {
           ret = test;
           num1 = num2;
         }
      }
      return ret;
    }
  }
}
