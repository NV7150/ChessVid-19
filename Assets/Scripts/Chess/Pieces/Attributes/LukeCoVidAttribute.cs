using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class LukeCoVidAttribute : CoVidAttributeBase {
        private static readonly List<MoveInfo> MOD_INFOS = new List<MoveInfo>();

        static LukeCoVidAttribute() {
            for (int i = 0; i <= 1; i++) {
                for (int front = -1; front <= 1; front += 2) {
                    var x = (i == 0) ? front : 0;
                    var y = (i != 0) ? front : 0;
                    MOD_INFOS.Add(new MoveInfo(new BoardVector(x, y),1));
                }
            }
        }

        public override string getAttributeName() {
            return "luke-covid";
        }

        public override Type getAttributeType() {
            return GetType();
        }

        public override List<MoveInfo> getModifiedMoveInfo() {
            return new List<MoveInfo>(MOD_INFOS);
        }
    }
}
