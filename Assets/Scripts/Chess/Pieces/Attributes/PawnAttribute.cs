using System;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class PawnAttribute : PieceAttribute {
        private BoardVector _startPos;
        
        public string getAttributeName() {
            return "Pawn";
        }

        public Type getAttributeType() {
            return this.GetType();
        }

        public void setStartPos(BoardVector startPos) {
            _startPos = startPos;
        }
        
        public bool canMoveTwice(BoardVector currPos) {
            return currPos == _startPos;
        }
    }
}
