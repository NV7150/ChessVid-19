using System;
using System.Collections.Generic;
using Chess.Pieces;
using UnityEngine;
using static Chess.Boards.SquareStatus;

namespace Chess.Boards {
    public interface Board : GameObjectInterface {
        Vector2 getStartScreenPos();
        BoardVector getBoardScale();
        Vector2 getBoardRealScale();
        Vector2 getSquareScale();
        float getScreenScaleRate();
        Piece getPiece(BoardVector pos);
        void movePiece(BoardVector prevPos, BoardVector newPos);
        void takePiece(BoardVector piecePos);
        void updateTurn();
        SquareStatus getStatus(int id, BoardVector pos);
    }

    public enum SquareStatus {
        ALLY,
        ENEMY,
        FREE,
        OUT_BOARD
    }

}
