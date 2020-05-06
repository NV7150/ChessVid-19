using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.Pieces.Attributes;
using UnityEngine;

namespace Chess.Pieces {
    public class Pawn : PieceBase {
        private readonly List<MoveInfo> _moveInfos = new List<MoveInfo>();

        void initMoveInfo() {
            _moveInfos.Add(new MoveInfo(new BoardVector(0, 1), 1));
            addAttribute(new PawnAttribute());
        }

        protected override void initPiece() {
            base.initPiece();
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
