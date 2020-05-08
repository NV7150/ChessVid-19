using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class KingCoVidAttribute : CoVidAttributeBase {
        private int _deathCount = 3;
        
        public override string getAttributeName() {
            return "king-covid";
        }

        public override Type getAttributeType() {
            return GetType();
        }

        public override List<MoveInfo> getModifiedMoveInfo() {
            return new List<MoveInfo>();
        }

        public override void updateTurn(int turn) {
            base.updateTurn(turn);
            if (Stage == 3 && !getAntiStatus(AntiStatus.ANTI_PROGRESS)) {
                _deathCount--;
                if (_deathCount <= 0)
                    IsDead = true;
            } else {
                _deathCount = 3;
            }
            
        }
    }
}
