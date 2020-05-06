using Chess.Boards;
using UnityEngine;

namespace Chess.GameSystems {
    public interface ChessManager {
        void placeSelected(BoardVector pos);
        void updateTurn();
        int getTurn();
        ChessPlayer getCurrentPlayer();
    }
}
