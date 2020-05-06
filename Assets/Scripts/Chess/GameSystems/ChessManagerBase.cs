using Chess.Boards;
using UnityEngine;

namespace Chess.GameSystems {
    public abstract class ChessManagerBase : MonoBehaviour, ChessManager {
        public abstract void placeSelected(BoardVector pos);

        public abstract void updateTurn();

        public abstract int getTurn();

        public abstract ChessPlayer getCurrentPlayer();
    }
}
