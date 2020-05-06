using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.GameSystems;
using Chess.Pieces.Attributes;
using UnityEngine;

namespace Chess.Pieces {
    public interface Piece : GameObjectInterface {
        ChessPlayer getOwner();
        T getAttribute<T>() where T : PieceAttribute;
        bool hasAttribute<T>() where T : PieceAttribute;
        List<MoveInfo> getMoveInfos();
        void turnEndCheck(BoardVector currPos);
        void moveTo(Vector2 nextScreenPos);
        void moveTo(Vector2 boardStart, Vector2 squareLength, BoardVector nextPos, float screenScaleRate);
        void takePiece(BoardVector tookPos);
    }
}
