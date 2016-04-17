﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.UI.IGameCanvas
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using System.Drawing;

namespace djack.RogueSurvivor.UI
{
  public interface IGameCanvas
  {
    bool ShowFPS { get; set; }

    bool NeedRedraw { get; set; }

    Point MouseLocation { get; set; }

    float ScaleX { get; }

    float ScaleY { get; }

    void BindForm(RogueForm form);

    void FillGameForm();

    void Clear(Color clearColor);

    void AddImage(Image img, int x, int y);

    void AddImage(Image img, int x, int y, Color tint);

    void AddImageTransform(Image img, int x, int y, float rotation, float scale);

    void AddTransparentImage(float alpha, Image img, int x, int y);

    void AddPoint(Color color, int x, int y);

    void AddLine(Color color, int xFrom, int yFrom, int xTo, int yTo);

    void AddRect(Color color, Rectangle rect);

    void AddFilledRect(Color color, Rectangle rect);

    void AddString(Font font, Color color, string text, int gx, int gy);

    void ClearMinimap(Color color);

    void SetMinimapColor(int x, int y, Color color);

    void DrawMinimap(int gx, int gy);

    string SaveScreenShot(string filePath);

    string ScreenshotExtension();

    void DisposeUnmanagedResources();
  }
}
