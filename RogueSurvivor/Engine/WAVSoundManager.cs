﻿// Decompiled with JetBrains decompiler
// Type: djack.RogueSurvivor.Engine.NullSoundManager
// Assembly: RogueSurvivor, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D2AE4FAE-2CA8-43FF-8F2F-59C173341976
// Assembly location: C:\Private.app\RS9Alpha.Hg\RogueSurvivor.exe

using System;
using System.Media;
using System.Collections.Generic;

namespace djack.RogueSurvivor.Engine
{
  internal class WAVSoundManager : ISoundManager,IDisposable
    {
    private readonly Dictionary<string, SoundPlayer> m_Musics = new Dictionary<string, SoundPlayer>();
    private readonly Dictionary<string, SoundPlayer> m_PlayingMusics = new Dictionary<string, SoundPlayer>();

    public bool IsMusicEnabled { get; set; }
    public int Volume { get; set; }

    public WAVSoundManager()
    {
      Volume = 100;
    }

    private string FullName(string fileName)
    {
      return fileName + ".wav";
    }


    public bool Load(string musicname, string filename)
    {
      filename = FullName(filename);
      Logger.WriteLine(Logger.Stage.INIT_SOUND, string.Format("loading music {0} file {1}", (object) musicname, (object) filename));
      try {
        SoundPlayer tmp = new SoundPlayer(filename);
        tmp.LoadAsync();    // default timeout is 10 seconds
        m_Musics.Add(musicname, tmp);
      } catch (Exception ex) {
        Logger.WriteLine(Logger.Stage.INIT_SOUND, string.Format("failed to load music file {0} exception {1}.", (object) filename, (object) ex.ToString()));
      }
      return true;
    }

    public void Unload(string musicname)
    {
      m_Musics.Remove(musicname);
      m_PlayingMusics.Remove(musicname);
    }

    public void Play(string musicname)
    {
      if (!IsMusicEnabled) return;
      SoundPlayer audio;
      if (!m_Musics.TryGetValue(musicname, out audio)) return;
      Logger.WriteLine(Logger.Stage.RUN_SOUND, string.Format("playing music {0}.", (object) musicname));
      audio.Play();
      m_PlayingMusics[musicname] = audio;
    }

    public void PlayIfNotAlreadyPlaying(string musicname)
    {
      if (!IsMusicEnabled) return;
      SoundPlayer audio;
      if (m_PlayingMusics.TryGetValue(musicname, out audio)) return;
      if (!m_Musics.TryGetValue(musicname, out audio)) return;
      Logger.WriteLine(Logger.Stage.RUN_SOUND, string.Format("playing music {0}.", (object) musicname));
      audio.Play();
    }

    public void PlayLooping(string musicname)
    {
      if (!IsMusicEnabled) return;
      SoundPlayer audio;
      if (!m_Musics.TryGetValue(musicname, out audio)) return;
      Logger.WriteLine(Logger.Stage.RUN_SOUND, string.Format("playing looping music {0}.", (object) musicname));
      audio.PlayLooping();
      m_PlayingMusics[musicname] = audio;
    }

    // no distinct resume
    public void ResumeLooping(string musicname)
    {
      if (!IsMusicEnabled) return;
      SoundPlayer audio;
      if (!m_Musics.TryGetValue(musicname, out audio)) return;
      audio.PlayLooping();
      m_PlayingMusics[musicname] = audio;
    }

    public void Stop(string musicname)
    {
      if (!IsMusicEnabled) return;
      SoundPlayer audio;
      if (!m_Musics.TryGetValue(musicname, out audio)) return;
      audio.Stop();
      m_PlayingMusics.Remove(musicname);
    }

    public void StopAll()
    {
      Logger.WriteLine(Logger.Stage.RUN_SOUND, "stopping all musics.");
      foreach (SoundPlayer audio in m_Musics.Values) { 
        audio.Stop();
      }
      m_PlayingMusics.Clear();
    }

    public bool IsPlaying(string musicname)
    {
      if (!IsMusicEnabled) return false;
      SoundPlayer audio;
      if (!m_PlayingMusics.TryGetValue(musicname, out audio)) return false;
      return true;
    }

    // not meaningful
    public bool IsPaused(string musicname)
    {
      return false;
    }

    // not meaningful
    public bool HasEnded(string musicname)
    {
      return true;
    }

    protected void Dispose(bool disposing)
    {
      if (!disposing) return;
      Logger.WriteLine(Logger.Stage.CLEAN_SOUND, "disposing WAVSoundManager...");
      foreach (string key in m_Musics.Keys) {
        SoundPlayer tmp = m_Musics[key];
        if (null == tmp) continue;
        Logger.WriteLine(Logger.Stage.CLEAN_SOUND, string.Format("disposing music {0}.", (object) key));
        tmp.Dispose();
      }
      m_Musics.Clear();
      m_PlayingMusics.Clear();
      Logger.WriteLine(Logger.Stage.CLEAN_SOUND, "disposing WAVSoundManager done.");
    }

    public void Dispose() { Dispose(true); }
  }
}
