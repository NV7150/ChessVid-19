using System;
using System.Collections.Generic;
using Chess.Pieces;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Boards {
    [Serializable]
    public class ListBoard : BoardBase {
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;
        [SerializeField] private int numOfSquare = 8;
        [SerializeField] private float squareWidth;

        private List<List<Piece>> _pieces;
        
        public void initializeBoard(List<List<Piece>> pieces) {
            if (_pieces != null)
                return;
            _pieces = pieces;
            
        }
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {

        }
        public override Vector2 getStartScreenPos() {
            return startPos.position;
        }

        public override BoardVector getBoardScale() {
            return new BoardVector(numOfSquare, numOfSquare);
        }

        public override Vector2 getBoardRealScale() {
            return endPos.position - startPos.position;
        }

        public override Vector2 getSquareScale() {
            return new Vector2(squareWidth, squareWidth);
        }

        public override Piece getPiece(BoardVector pos) {
            return _pieces[pos.x][pos.y];
        }

        public override void movePiece(BoardVector prevPos, BoardVector newPos) {
            var piece = getPiece(prevPos);
            takePiece(newPos);
            piece.moveTo(getStartScreenPos(), getSquareScale(), newPos, getScreenScaleRate());
            _pieces[newPos.x][newPos.y] = piece;
            _pieces[prevPos.x][prevPos.y] = null;
        }

        public override void takePiece(BoardVector piecePos) {
            var piece = getPiece(piecePos);
            if (piece == null)
                return;
            _pieces[piecePos.x][piecePos.y] = null;
            piece.takePiece(piecePos);
        }

        public override void updateTurn() {
            //今んとこなし
        }

        public override SquareStatus getStatus(int id, BoardVector pos) {
            if (pos.x < 0 || numOfSquare <= pos.x || pos.y < 0 || numOfSquare <= pos.y)
                return SquareStatus.OUT_BOARD;
            
            var piece = getPiece(pos);
            if (piece == null)
                return SquareStatus.FREE;

            return (piece.getOwner().getId() == id) ? SquareStatus.ALLY : SquareStatus.ENEMY;
        }
    }
}
