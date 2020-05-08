using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.GameSystems;
using Chess.Pieces.Attributes;
using UnityEngine;

namespace Chess.Pieces {
    public abstract class  PieceBase : MonoBehaviour, Piece {
        private ChessPlayer _chessPlayer;
        private readonly List<PieceAttribute> _pieceAttributes = new List<PieceAttribute>();

        public void initializePiece(ChessPlayer player) {
            _chessPlayer = player;
            initPiece();
        }
        
        public void addAttribute(PieceAttribute attribute ){
            _pieceAttributes.Add(attribute);
        }

        protected virtual void initPiece() {
            
        }
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public GameObject getGameObject() {
            return gameObject;
        }

        public ChessPlayer getOwner() {
            return _chessPlayer;
        }

        public virtual void moveTo(Vector2 nextScreenPos) {
            transform.position = nextScreenPos;
        }

        public void moveTo(Vector2 boardStart, Vector2 squareLength, BoardVector nextPos, float screenScaleRate) {
            var deltaVec = new Vector2(
                getRealDeltaPos(squareLength.x, nextPos.x, screenScaleRate), 
                getRealDeltaPos(squareLength.y, nextPos.y, screenScaleRate));
            
            var realVec = boardStart + deltaVec;
            moveTo(realVec);
        }

        float getRealDeltaPos(float squareLength, float boardVecPos, float screenScaleRate) {
            return squareLength * screenScaleRate * boardVecPos + (squareLength * screenScaleRate / 2);
        }

        public virtual void takePiece(BoardVector tookPos) {
            _chessPlayer.setPieceNum(_chessPlayer.getPieceNum() - 1);
            Destroy(gameObject);
        }

        public virtual T getAttribute<T>() where T : PieceAttribute {
            foreach (var attr in _pieceAttributes) {
                if (attr is T attr1) {
                    return attr1;
                }
            }

            throw new ArgumentException();
        }

        public virtual bool hasAttribute<T>() where T : PieceAttribute {
            foreach (var attr in _pieceAttributes) {
                if (attr is T) {
                    return true;
                }
            }

            return false;
        }

        public abstract List<MoveInfo> getMoveInfos();

        public abstract void turnEndCheck(BoardVector currPos);
    }
}
