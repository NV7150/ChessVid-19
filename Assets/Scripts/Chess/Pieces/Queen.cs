using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces {
    public class Queen : PieceBase {
        private static readonly List<MoveInfo> MOVE_INFOS = new List<MoveInfo>();

        static Queen() {
            initMoveInfo();
        }

        static void initMoveInfo() {
            if (MOVE_INFOS.Count > 0)
                return;
            
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if(x == 0 && y == 0)
                        continue;
                    MOVE_INFOS.Add(new MoveInfo(new BoardVector(x, y), 1, isEndless:true));
                }
            }
        }

        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public override List<MoveInfo> getMoveInfos() {
            return new List<MoveInfo>(MOVE_INFOS);
        }

        public override void turnEndCheck(BoardVector currPos) {
            //NONE
        }
    }
}
