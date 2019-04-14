﻿using System;
using System.Collections.Generic;
using System.Linq;
using djack.RogueSurvivor.Data;

namespace djack.RogueSurvivor.Engine.Actions
{
    [Serializable]
    internal class ActionSequence : ActorAction
    {
        private int _AP;
        private int _turn;
        private List<int> _handler_codes;
        private ActorAction _result;

        public ActionSequence(Actor actor, IEnumerable<int> codes)
        : base(actor)
        {
#if DEBUG
            if (null == codes || !codes.Any()) throw new ArgumentNullException(nameof(codes));
#endif
            _AP = m_Actor.ActionPoints;
            _turn = m_Actor.Location.Map.LocalTime.TurnCounter;
            _handler_codes = codes.ToList();
        }

        public override bool IsLegal()
        {
            if (_AP <= m_Actor.ActionPoints && _turn >= m_Actor.Location.Map.LocalTime.TurnCounter) return true;
            if (0 < _handler_codes.Count && !m_Actor.Controller.IsMyTurn()) return true;
            return (_result ?? (_result = _resolve()))?.IsLegal() ?? false;
        }

        public override void Perform()
        {
            (_result ?? (_result = _resolve()))?.Perform();
        }

        private ActorAction _resolve()
        {
            ActorAction working = null;
            while(0 < _handler_codes.Count) {
                working = m_Actor.Controller.ExecAryZeroBehavior(_handler_codes[0]);
                if (null != working && working.IsLegal()) return working;
                // \todo we're using a list so we can append further handlers
                _handler_codes.RemoveAt(0);
            }
            return null;
        }
    }
}