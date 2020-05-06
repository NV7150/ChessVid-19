using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces {
    public class Queen : PieceBase {
        private readonly List<MoveInfo> _moveInfos = new List<MoveInfo>();

        void initMoveInfo() {
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if(x == 0 && y == 0)
                        continue;
                    _moveInfos.Add(new MoveInfo(new BoardVector(x, y), 1, isEndless:true));
                }
            }
        }

        private void Awake() {
            initMoveInfo();
        }

        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public override List<MoveInfo> getMoveInfos() {
            return new List<MoveInfo>(_moveInfos);
        }

        public override void turnEndCheck(BoardVector currPos) {
            //NONE
        }
    }
}
