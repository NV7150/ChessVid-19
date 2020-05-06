using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.Pieces.Attributes;
using UnityEngine;

namespace Chess.Pieces {
    public class Knight : PieceBase {
        private readonly List<MoveInfo> _moveInfos = new List<MoveInfo>();

        void initMoveInfo() {
            for (int i = 0; i <= 1; i++) {
                for (int front = -2; front <= 2; front += 4) {
                    for (int side = -1; side <= 1; side += 2) {
                        var x = (i == 0) ? front : side;
                        var y = (i != 0) ? front : side;
                        _moveInfos.Add(new MoveInfo(new BoardVector(x, y), 1, isJump: true));
                    }
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
