using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class PawnCoVidAttribute : CoVidAttributeBase {
        public override string getAttributeName() {
            return "pawn-covid";
        }

        public override Type getAttributeType() {
            return GetType();
        }

        public override bool useModifiedMoveInfo() {
            return false;
        }

        public override List<MoveInfo> getModifiedMoveInfo() {
            throw new MethodAccessException("invalid access");
        }

        protected override float getInfectivity() {
            switch (getStage()) {
                case 1:
                    return 30f;
                case 2:
                    return 50f;
                case 3:
                    return 70f;
            }

            return 0;
        }
    }
}
