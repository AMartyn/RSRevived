﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.Scoring
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using djack.RogueSurvivor.Data;
using djack.RogueSurvivor.Gameplay;
using System;
using System.Collections.Generic;

namespace djack.RogueSurvivor.Engine
{
  [Serializable]
  internal class Scoring
  {
    private Dictionary<int, Scoring.KillData> m_Kills = new Dictionary<int, Scoring.KillData>();
    private HashSet<int> m_Sightings = new HashSet<int>();
    private List<Scoring.GameEventData> m_Events = new List<Scoring.GameEventData>();
    private HashSet<Map> m_VisitedMaps = new HashSet<Map>();
    private float m_DifficultyRating = 1f;
    public const int MAX_ACHIEVEMENTS = 8;
    public const int SCORE_BONUS_FOR_KILLING_LIVING_AS_UNDEAD = 360;
    private int m_StartScoringTurn;
    private int m_ReincarnationNumber;
    private List<Actor> m_FollowersWhenDied;
    private Actor m_Killer;
    private Actor m_ZombifiedPlayer;
    private int m_KillPoints;
    private DifficultySide m_Side;

    public DifficultySide Side
    {
      get
      {
        return m_Side;
      }
      set
      {
                m_Side = value;
      }
    }

    public int StartScoringTurn
    {
      get
      {
        return m_StartScoringTurn;
      }
      set
      {
                m_StartScoringTurn = value;
      }
    }

    public int ReincarnationNumber
    {
      get
      {
        return m_ReincarnationNumber;
      }
      set
      {
                m_ReincarnationNumber = value;
      }
    }

    public Achievement[] Achievements { get; private set; }

    public Skills.IDs StartingSkill { get; set; }

    public IEnumerable<Scoring.GameEventData> Events
    {
      get
      {
        return (IEnumerable<Scoring.GameEventData>)m_Events;
      }
    }

    public bool HasNoEvents
    {
      get
      {
        return m_Events.Count == 0;
      }
    }

    public IEnumerable<Scoring.KillData> Kills
    {
      get
      {
        return (IEnumerable<Scoring.KillData>)m_Kills.Values;
      }
    }

    public bool HasNoKills
    {
      get
      {
        return m_Kills.Count == 0;
      }
    }

    public IEnumerable<int> Sightings
    {
      get
      {
        return (IEnumerable<int>)m_Sightings;
      }
    }

    public int TurnsSurvived { get; set; }

    public string DeathReason { get; set; }

    public string DeathPlace { get; set; }

    public List<Actor> FollowersWhendDied
    {
      get
      {
        return m_FollowersWhenDied;
      }
    }

    public Actor Killer
    {
      get
      {
        return m_Killer;
      }
    }

    public Actor ZombifiedPlayer
    {
      get
      {
        return m_ZombifiedPlayer;
      }
    }

    public int KillPoints
    {
      get
      {
        return m_KillPoints;
      }
    }

    public int SurvivalPoints
    {
      get
      {
        return 2 * (TurnsSurvived - m_StartScoringTurn);
      }
    }

    public int AchievementPoints
    {
      get
      {
        int num = 0;
        for (int index = 0; index < 8; ++index)
        {
          if (HasCompletedAchievement((Achievement.IDs) index))
            num += GetAchievement((Achievement.IDs) index).ScoreValue;
        }
        return num;
      }
    }

    public float DifficultyRating
    {
      get
      {
        return m_DifficultyRating / (float) (1 + m_ReincarnationNumber);
      }
      set
      {
                m_DifficultyRating = value;
      }
    }

    public int TotalPoints
    {
      get
      {
        return (int) ((double)DifficultyRating * (double) (m_KillPoints + SurvivalPoints + AchievementPoints));
      }
    }

    public TimeSpan RealLifePlayingTime { get; set; }

    public int CompletedAchievementsCount { get; set; }

    public Scoring()
    {
            RealLifePlayingTime = new TimeSpan(0L);
            Achievements = new Achievement[8];
            InitAchievement(Achievement.IDs.CHAR_BROKE_INTO_OFFICE, new Achievement(Achievement.IDs.CHAR_BROKE_INTO_OFFICE, "Broke into a CHAR Office", "Did not broke into XXX", new string[1]
      {
        "Now try not to die too soon..."
      }, GameMusics.HEYTHERE, 1000));
            InitAchievement(Achievement.IDs.CHAR_FOUND_UNDERGROUND_FACILITY, new Achievement(Achievement.IDs.CHAR_FOUND_UNDERGROUND_FACILITY, "Found the CHAR Underground Facility", "Did not found XXX", new string[1]
      {
        "Now, where is the light switch?..."
      }, GameMusics.CHAR_UNDERGROUND_FACILITY, 2000));
            InitAchievement(Achievement.IDs.CHAR_POWER_UNDERGROUND_FACILITY, new Achievement(Achievement.IDs.CHAR_POWER_UNDERGROUND_FACILITY, "Powered the CHAR Underground Facility", "Did not XXX the XXX", new string[5]
      {
        "Personal message from the game developper : ",
        "Sorry, the rest of the plot is missing.",
        "For now its a dead end.",
        "Enjoy the rest of the game.",
        "See you in a next game version :)"
      }, GameMusics.CHAR_UNDERGROUND_FACILITY, 3000));
            InitAchievement(Achievement.IDs.KILLED_THE_SEWERS_THING, new Achievement(Achievement.IDs.KILLED_THE_SEWERS_THING, "Killed The Sewers Thing", "Did not kill the XXX", new string[1]
      {
        "One less Thing to worry about!"
      }, GameMusics.HEYTHERE, 1000));
            InitAchievement(Achievement.IDs._FIRST, new Achievement(Achievement.IDs._FIRST, "Reached Day 7", "Did not reach XXX", new string[1]
      {
        "Keep staying alive!"
      }, GameMusics.HEYTHERE, 1000));
            InitAchievement(Achievement.IDs.REACHED_DAY_14, new Achievement(Achievement.IDs.REACHED_DAY_14, "Reached Day 14", "Did not reach XXX", new string[1]
      {
        "Keep staying alive!"
      }, GameMusics.HEYTHERE, 1000));
            InitAchievement(Achievement.IDs.REACHED_DAY_21, new Achievement(Achievement.IDs.REACHED_DAY_21, "Reached Day 21", "Did not reach XXX", new string[1]
      {
        "Keep staying alive!"
      }, GameMusics.HEYTHERE, 1000));
            InitAchievement(Achievement.IDs.REACHED_DAY_28, new Achievement(Achievement.IDs.REACHED_DAY_28, "Reached Day 28", "Did not reach XXX", new string[1]
      {
        "Is this the end?"
      }, GameMusics.HEYTHERE, 1000));
    }

    public void StartNewLife(int gameTurn)
    {
      ++m_ReincarnationNumber;
      foreach (Achievement achievement in Achievements)
        achievement.IsDone = false;
            CompletedAchievementsCount = 0;
            m_VisitedMaps.Clear();
            m_Events.Clear();
            m_Sightings.Clear();
            m_Kills.Clear();
            m_Killer = (Actor) null;
            m_FollowersWhenDied = (List<Actor>) null;
            m_ZombifiedPlayer = (Actor) null;
            m_KillPoints = 0;
            m_StartScoringTurn = gameTurn;
    }

    public bool HasCompletedAchievement(Achievement.IDs id)
    {
      return Achievements[(int) id].IsDone;
    }

    public void SetCompletedAchievement(Achievement.IDs id)
    {
            Achievements[(int) id].IsDone = true;
    }

    public Achievement GetAchievement(Achievement.IDs id)
    {
      return Achievements[(int) id];
    }

    private void InitAchievement(Achievement.IDs id, Achievement a)
    {
            Achievements[(int) id] = a;
    }

    public static float ComputeDifficultyRating(GameOptions options, DifficultySide side, int reincarnationNumber)
    {
      float num1 = 1f;
      if (!options.RevealStartingDistrict)
        num1 += 0.1f;
      if (!options.NPCCanStarveToDeath)
      {
        if (side == DifficultySide.FOR_SURVIVOR)
          num1 -= 0.1f;
        else
          num1 += 0.1f;
      }
      if (options.NatGuardFactor != GameOptions.DEFAULT_NATGUARD_FACTOR)
      {
        float num2 = (float) (options.NatGuardFactor - GameOptions.DEFAULT_NATGUARD_FACTOR) / (float)GameOptions.DEFAULT_NATGUARD_FACTOR;
        if (side == DifficultySide.FOR_SURVIVOR)
          num1 -= 0.5f * num2;
        else
          num1 += 0.5f * num2;
      }
      if (options.SuppliesDropFactor != GameOptions.DEFAULT_SUPPLIESDROP_FACTOR)
      {
        float num2 = (float) (options.SuppliesDropFactor - GameOptions.DEFAULT_SUPPLIESDROP_FACTOR) / (float)GameOptions.DEFAULT_SUPPLIESDROP_FACTOR;
        if (side == DifficultySide.FOR_SURVIVOR)
          num1 -= 0.5f * num2;
        else
          num1 += 0.5f * num2;
      }
      if (options.ZombifiedsUpgradeDays != GameOptions.ZupDays.THREE)
      {
        float num2 = 0.0f;
        switch (options.ZombifiedsUpgradeDays)
        {
          case GameOptions.ZupDays._FIRST:
            num2 = 0.5f;
            break;
          case GameOptions.ZupDays.TWO:
            num2 = 0.25f;
            break;
          case GameOptions.ZupDays.FOUR:
            num2 -= 0.1f;
            break;
          case GameOptions.ZupDays.FIVE:
            num2 -= 0.2f;
            break;
          case GameOptions.ZupDays.SIX:
            num2 -= 0.3f;
            break;
          case GameOptions.ZupDays.SEVEN:
            num2 -= 0.4f;
            break;
          case GameOptions.ZupDays.OFF:
            num2 = -0.5f;
            break;
        }
        if (side == DifficultySide.FOR_SURVIVOR)
          num1 += num2;
        else
          num1 -= num2;
      }
      float num3 = (float) Math.Sqrt(125.0) / 12500f;
      float num4 = ((float) Math.Sqrt((double) (options.MaxCivilians + options.MaxUndeads)) / (float) (options.CitySize * options.DistrictSize * options.DistrictSize) - num3) / num3;
      float num5 = side != DifficultySide.FOR_SURVIVOR ? num1 - 0.99f * num4 : num1 + 0.99f * num4;
      float num6 = 4f;
      float num7 = ((float) options.MaxUndeads / (float) options.MaxCivilians - num6) / num6;
      float num8 = (float) (options.DayZeroUndeadsPercent - GameOptions.DEFAULT_DAY_ZERO_UNDEADS_PERCENT) / (float)GameOptions.DEFAULT_DAY_ZERO_UNDEADS_PERCENT;
      float num9 = (float) (options.ZombieInvasionDailyIncrease - 5) / 5f;
      float num10 = side != DifficultySide.FOR_SURVIVOR ? num5 - (float) (0.3 * (double) num7 + 0.05 * (double) num8 + 0.15 * (double) num9) : num5 + (float) (0.3 * (double) num7 + 0.05 * (double) num8 + 0.15 * (double) num9);
      float num11 = 2500f;
      float num12 = ((float) (options.MaxCivilians * options.ZombificationChance) - num11) / num11;
      float num13 = 1250f;
      float num14 = ((float) (options.MaxCivilians * options.StarvedZombificationChance) - num13) / num13;
      if (!options.NPCCanStarveToDeath)
        num14 = -1f;
      float num15 = side != DifficultySide.FOR_SURVIVOR ? num10 - (float) (0.3 * (double) num12 + 0.2 * (double) num14) : num10 + (float) (0.3 * (double) num12 + 0.2 * (double) num14);
      if (!options.AllowUndeadsEvolution)
      {
        if (side == DifficultySide.FOR_SURVIVOR)
          num15 *= 0.5f;
        else
          num15 *= 2f;
      }
      if (options.IsCombatAssistantOn)
        num15 *= 0.75f;
      if (options.IsPermadeathOn)
        num15 *= 2f;
      if (!options.IsAggressiveHungryCiviliansOn)
      {
        if (side == DifficultySide.FOR_SURVIVOR)
          num15 *= 0.5f;
        else
          num15 *= 2f;
      }
      if (options.RatsUpgrade)
      {
        if (side == DifficultySide.FOR_SURVIVOR)
          num15 *= 1.1f;
        else
          num15 *= 0.9f;
      }
      if (options.SkeletonsUpgrade)
      {
        if (side == DifficultySide.FOR_SURVIVOR)
          num15 *= 1.2f;
        else
          num15 *= 0.8f;
      }
      if (options.ShamblersUpgrade)
      {
        if (side == DifficultySide.FOR_SURVIVOR)
          num15 *= 1.25f;
        else
          num15 *= 0.75f;
      }
      return Math.Max(num15 / (float) (1 + reincarnationNumber), 0.0f);
    }

    public void AddKill(Actor player, Actor victim, int turn)
    {
      int id = victim.Model.ID;
      Scoring.KillData killData;
      if (m_Kills.TryGetValue(id, out killData))
      {
        ++killData.Amount;
      }
      else
      {
                m_Kills.Add(id, new Scoring.KillData(id, turn));
                m_Events.Add(new Scoring.GameEventData(turn, string.Format("Killed first {0}.", (object) Models.Actors[id].Name)));
      }
            m_KillPoints += Models.Actors[id].ScoreValue;
      if (m_Side != DifficultySide.FOR_UNDEAD || Models.Actors[id].Abilities.IsUndead)
        return;
      m_KillPoints += SCORE_BONUS_FOR_KILLING_LIVING_AS_UNDEAD;
    }

    public void AddSighting(int actorModelID, int turn)
    {
      if (m_Sightings.Contains(actorModelID))
        return;
            m_Sightings.Add(actorModelID);
            m_Events.Add(new Scoring.GameEventData(turn, string.Format("Sighted first {0}.", (object) Models.Actors[actorModelID].Name)));
    }

    public bool HasSighted(int actorModelID)
    {
      return m_Sightings.Contains(actorModelID);
    }

    public bool HasVisited(Map map)
    {
      return m_VisitedMaps.Contains(map);
    }

    public void AddVisit(int turn, Map map)
    {
      lock (m_VisitedMaps)
                m_VisitedMaps.Add(map);
    }

    public void SetKiller(Actor k)
    {
            m_Killer = k;
    }

    public void SetZombifiedPlayer(Actor z)
    {
            m_ZombifiedPlayer = z;
    }

    public void AddFollowerWhenDied(Actor fo)
    {
      if (m_FollowersWhenDied == null)
                m_FollowersWhenDied = new List<Actor>();
            m_FollowersWhenDied.Add(fo);
    }

    public void AddEvent(int turn, string text)
    {
      lock (m_Events)
                m_Events.Add(new Scoring.GameEventData(turn, text));
    }

    [Serializable]
    public class KillData
    {
      public int ActorModelID { get; set; }

      public int Amount { get; set; }

      public int FirstKillTurn { get; set; }

      public KillData(int actorModelID, int turn)
      {
                ActorModelID = actorModelID;
                Amount = 1;
                FirstKillTurn = turn;
      }
    }

    [Serializable]
    public class GameEventData
    {
      public int Turn { get; set; }

      public string Text { get; set; }

      public GameEventData(int turn, string text)
      {
                Turn = turn;
                Text = text;
      }
    }
  }
}
