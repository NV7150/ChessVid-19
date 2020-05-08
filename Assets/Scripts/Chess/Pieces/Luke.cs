using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces {
    public class Luke : PieceBase {
        private static readonly List<MoveInfo> MOVE_INFOS = new List<MoveInfo>();

        static Luke() {
            initMoveInfo();
        }

        static void initMoveInfo() {
            if (MOVE_INFOS.Count > 0)
                return;
            
            for (int i = 0; i <= 1; i++) {
                for (int front = -1; front <= 1; front += 2) {
                    var x = (i == 0) ? front : 0;
                    var y = (i != 0) ? front : 0;
                    MOVE_INFOS.Add(new MoveInfo(new BoardVector(x, y), 1, isEndless:true));
                }
            }
        }

        private void Awake() {
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
