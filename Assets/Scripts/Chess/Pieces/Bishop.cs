using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces {
    public class Bishop : PieceBase {
        private static readonly List<MoveInfo> MOVE_INFOS = new List<MoveInfo>();

        static Bishop() {
            initMoveInfo();
        }

        static void initMoveInfo() {
            if (MOVE_INFOS.Count > 0)
                return;
            
            for (int x = -1; x <= 1; x += 2) {
                for (int y = -1; y <= 1; y += 2) {
                    MOVE_INFOS.Add(new MoveInfo(new BoardVector(x, y), 1 , isEndless:true));
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
