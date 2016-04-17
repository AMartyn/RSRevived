﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.HiScoreTable
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace djack.RogueSurvivor.Engine
{
  [Serializable]
  internal class HiScoreTable
  {
    public const int DEFAULT_MAX_ENTRIES = 12;
    private List<HiScore> m_Table;
    private int m_MaxEntries;

    public int Count
    {
      get
      {
        return this.m_Table.Count;
      }
    }

    public HiScore this[int index]
    {
      get
      {
        return this.Get(index);
      }
    }

    public HiScoreTable(int maxEntries)
    {
      if (maxEntries < 1)
        throw new ArgumentOutOfRangeException("maxEntries < 1");
      this.m_Table = new List<HiScore>(maxEntries);
      this.m_MaxEntries = maxEntries;
    }

    public void Clear()
    {
      for (int index = 0; index < this.m_MaxEntries; ++index)
        this.m_Table.Add(new HiScore()
        {
          Death = "no death",
          DifficultyPercent = 0,
          KillPoints = 0,
          Name = "no one",
          PlayingTime = TimeSpan.Zero,
          SurvivalPoints = 0,
          TotalPoints = 0,
          TurnSurvived = 0,
          SkillsDescription = "no skills"
        });
    }

    public bool Register(HiScore hi)
    {
      int index = 0;
      while (index < this.m_Table.Count && this.m_Table[index].TotalPoints >= hi.TotalPoints)
        ++index;
      if (index > this.m_MaxEntries)
        return false;
      this.m_Table.Insert(index, hi);
      while (this.m_Table.Count > this.m_MaxEntries)
        this.m_Table.RemoveAt(this.m_Table.Count - 1);
      return true;
    }

    public HiScore Get(int index)
    {
      if (index < 0 || index >= this.m_Table.Count)
        throw new ArgumentOutOfRangeException("index");
      return this.m_Table[index];
    }

    public static void Save(HiScoreTable table, string filepath)
    {
      if (table == null)
        throw new ArgumentNullException("table");
      if (filepath == null)
        throw new ArgumentNullException("filepath");
      Logger.WriteLine(Logger.Stage.RUN_MAIN, "saving hiscore table...");
      IFormatter formatter = HiScoreTable.CreateFormatter();
      Stream stream = HiScoreTable.CreateStream(filepath, true);
      formatter.Serialize(stream, (object) table);
      stream.Flush();
      stream.Close();
      Logger.WriteLine(Logger.Stage.RUN_MAIN, "saving hiscore table... done!");
    }

    public static HiScoreTable Load(string filepath)
    {
      if (filepath == null)
        throw new ArgumentNullException("filepath");
      Logger.WriteLine(Logger.Stage.RUN_MAIN, "loading hiscore table...");
      HiScoreTable hiScoreTable;
      try
      {
        IFormatter formatter = HiScoreTable.CreateFormatter();
        Stream stream = HiScoreTable.CreateStream(filepath, false);
        hiScoreTable = (HiScoreTable) formatter.Deserialize(stream);
        stream.Close();
      }
      catch (Exception ex)
      {
        Logger.WriteLine(Logger.Stage.RUN_MAIN, "failed to load hiscore table (no hiscores?).");
        Logger.WriteLine(Logger.Stage.RUN_MAIN, string.Format("load exception : {0}.", (object) ex.ToString()));
        return (HiScoreTable) null;
      }
      Logger.WriteLine(Logger.Stage.RUN_MAIN, "loading hiscore table... done!");
      return hiScoreTable;
    }

    private static IFormatter CreateFormatter()
    {
      return (IFormatter) new BinaryFormatter();
    }

    private static Stream CreateStream(string saveFileName, bool save)
    {
      return (Stream) new FileStream(saveFileName, save ? FileMode.Create : FileMode.Open, save ? FileAccess.Write : FileAccess.Read, FileShare.None);
    }
  }
}
