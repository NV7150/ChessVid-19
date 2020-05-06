using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.GameSystems.MoveSystem {
    public abstract class ChessMoveSystemBase : MonoBehaviour, ChessMoveSystem {
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public abstract List<BoardVector> getMovablePos(GameInfo info, BoardVector piecePos);
    }
}
