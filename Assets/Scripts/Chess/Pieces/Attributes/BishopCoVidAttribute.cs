using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class BishopCoVidAttribute : CoVidAttributeBase {
        private static readonly List<MoveInfo> MOVE_INFOS = new List<MoveInfo>();

        static BishopCoVidAttribute() {
            for (int x = -1; x <= 1; x += 2) {
                for(int y = -1; y <= 1; y += 2 ){
                    MOVE_INFOS.Add(new MoveInfo(new BoardVector(x, y), 1));
                }
            }
        }
        
        public override string getAttributeName() {
            return "bishop-covid";
        }

        public override Type getAttributeType() {
            return GetType();
        }

        public override List<MoveInfo> getModifiedMoveInfo() {
            return new List<MoveInfo>(MOVE_INFOS);
        }
    }
}
