using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class QueenCoVidAttribute : CoVidAttributeBase {
        public override string getAttributeName() {
            return "queen-covid";
        }

        public override Type getAttributeType() {
            return GetType();
        }

        public override List<MoveInfo> getModifiedMoveInfo() {
            return new List<MoveInfo>();
        }
    }
}
