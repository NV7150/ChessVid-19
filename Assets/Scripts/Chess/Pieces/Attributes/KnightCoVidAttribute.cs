using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class KnightCoVidAttribute : CoVidAttributeBase {
        private static readonly List<MoveInfo> MOD_INFOS = new List<MoveInfo>();

        static KnightCoVidAttribute() {
            for (int i = 0; i <= 1; i++) {
                for (int front = -2; front <= 2; front += 4) {
                    for (int side = -1; side <= 1; side += 2) {
                        var x = (i == 0) ? front : side;
                        var y = (i != 0) ? front : side;
                        MOD_INFOS.Add(new MoveInfo(new BoardVector(x, y), 1));
                    }
                }
            }
        }
        
        
        public override string getAttributeName() {
            return "knight-covid";
        }

        public override Type getAttributeType() {
            return GetType();
        }

        public override List<MoveInfo> getModifiedMoveInfo() {
            return new List<MoveInfo>(MOD_INFOS);
        }
    }
}
