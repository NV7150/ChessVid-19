using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.Pieces.Attributes;
using UnityEngine;

namespace Chess.Pieces {
    public class Pawn : PieceBase {
        private static readonly List<MoveInfo> MOVE_INFOS = new List<MoveInfo>();

        static Pawn() {
            initMoveInfo();
        }
        
        static void initMoveInfo() {
            if (MOVE_INFOS.Count > 0)
                return;
            MOVE_INFOS.Add(new MoveInfo(new BoardVector(0, 1), 1));
        }

        protected override void initPiece() {
            base.initPiece();
            addAttribute(new PawnAttribute());
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
