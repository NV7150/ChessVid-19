using System.Collections.Generic;
using Chess.Boards;
using Chess.Pieces;
using Chess.Pieces.Attributes;
using UnityEngine;

namespace Chess.GameSystems.MoveSystem {
    public class ChessVidMoveSystem : SimpleChessMoveSystem {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override List<BoardVector> getMovablePos(GameInfo info, BoardVector piecePos) {
            var piece = info.Board.getPiece(piecePos);
            bool useModMoveInfo = false;

            List<MoveInfo> movableInfo = new List<MoveInfo>();
            if (piece.hasAttribute<StayHomeAttribute>()) {
                var stayHomeAttr = piece.getAttribute<StayHomeAttribute>();
                if (stayHomeAttr.IsStayHome) {
                    movableInfo = stayHomeAttr.getStayHomeMoveInfo();
                    useModMoveInfo = true;
                }
            }

            if (!useModMoveInfo && piece.hasAttribute<CoVidAttribute>()) {
                var coVidAttr = piece.getAttribute<CoVidAttribute>();
                if (coVidAttr.useModifiedMoveInfo()) {
                    movableInfo = coVidAttr.getModifiedMoveInfo();
                    useModMoveInfo = true;
                }
            }

            if (useModMoveInfo) {
                var id = piece.getOwner().getId();
                var dir = info.getPlayer(id).FrontDirection;
                var allMovable = new List<BoardVector>();
                foreach (var moveInfo in movableInfo) {
                    allMovable.AddRange(BoardUtil.getInfoPos(moveInfo, dir, piecePos, info.Board, id));
                }

                return allMovable;
            }
            
            return base.getMovablePos(info, piecePos);
        }
    }
}
