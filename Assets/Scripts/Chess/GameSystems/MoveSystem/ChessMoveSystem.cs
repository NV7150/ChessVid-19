using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.GameSystems.MoveSystem {
    public interface ChessMoveSystem {
        List<BoardVector> getMovablePos(GameInfo info, BoardVector piecePos);
    }
}
