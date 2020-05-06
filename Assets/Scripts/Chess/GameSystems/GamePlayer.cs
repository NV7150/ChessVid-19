using System;
using Chess.Boards;
using UnityEngine;

namespace Chess.GameSystems {
    public class GamePlayer : MonoBehaviour, ChessPlayer {
        [SerializeField]private int id;
        [SerializeField] private BoardVector frontDirection;
        [SerializeField] private Color playerColor;
        private int _remainPieces = -1;
        private bool _kingTaken = false;
        
        public BoardVector FrontDirection => frontDirection;

        public Color PlayerColor => playerColor;
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public int getId() {
            return id;
        }

        public bool isLoser() {
            return (_kingTaken || _remainPieces == 0);
        }

        public int getPieceNum() {
            return _remainPieces;
        }

        public void setPieceNum(int num) {
            _remainPieces = num;
        }

        public void kingTaken() {
            _kingTaken = true;
        }
    }
}
