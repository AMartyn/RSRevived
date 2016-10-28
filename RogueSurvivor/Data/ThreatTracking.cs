﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace djack.RogueSurvivor.Data
{
    [Serializable]
    class ThreatTracking
    {
        // an earlier iteration of this cost 39MB of savefile size.  Instead of attempting a full probability analysis,
        // we'll just do taint checking.
        private Dictionary<Actor, HashSet<Location>> _threats;  // simpler taint tracking

        public ThreatTracking()
        {
          _threats = new Dictionary<Data.Actor, HashSet<Data.Location>>();
          Actor.Dies += HandleDie;  // XXX removal would be in destructor
          Actor.Moving += HandleMove;
        }

        public void Clear()
        {
          _threats.Clear();
        }

        public bool IsThreat(Actor a)
        {
          return _threats.ContainsKey(a);
        }

        public void RecordSpawn(Actor a, IEnumerable<Location> locs)
        {
          _threats[a] = new HashSet<Location>(locs);
        }

        public void RecordTaint(Actor a, Location loc)
        {
          if (!_threats.ContainsKey(a)) {
            _threats[a] = new HashSet<Location>();
            _threats[a].Add(loc);
          } else _threats[a].Add(loc);
        }

        public void Sighted(Actor a, Location loc)
        {
          _threats[a] = new HashSet<Location>();
          _threats[a].Add(loc);
        }

        public void Cleared(Location loc)
        {
          foreach (Actor a in new List<Actor>(_threats.Keys)) {
            if (_threats[a].Remove(loc) && 0 >= _threats[a].Count) _threats.Remove(a);
          }
        }

        public void Cleared(Actor a)
        {
          _threats.Remove(a);
        }

        // cheating die handler
        private void HandleDie(object sender, Actor.DieArgs e)
        {
           Actor fatality = (sender as Actor);
           if (null == fatality) throw new ArgumentNullException("fatality");
           _threats.Remove(fatality);
        }

        // cheating move handler
        private void HandleMove(object sender, EventArgs e)
        {
          Actor moving = (sender as Actor);
          if (null == moving) throw new ArgumentNullException("moving");
          if (!_threats.ContainsKey(moving)) return;
          List<Point> tmp = moving.OneStepRange(moving.Location.Map, moving.Location.Position);
          foreach(Point pt in tmp) {
            _threats[moving].Add(new Location(moving.Location.Map,pt));
          }
        }
    }
}
