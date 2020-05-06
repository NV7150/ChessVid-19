using System;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces {
    [Serializable]
    public class MoveInfo {
        [SerializeField] private BoardVector direction;
        [SerializeField] private int moveAmounts;
        [SerializeField] private bool isEndless;
        [SerializeField] private bool isJump;

        public MoveInfo() {
            
        }
        
        public MoveInfo(BoardVector direction, int moveAmounts, bool isEndless = false, bool isJump = false)  {
            this.direction = direction;
            this.moveAmounts = moveAmounts;
            this.isEndless = isEndless;
            this.isJump = isJump;
        }

        public BoardVector Direction => direction;

        public int MoveAmounts => moveAmounts;

        public bool IsEndless => isEndless;

        public bool IsJump => isJump;
    }
}
