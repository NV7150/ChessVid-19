using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.Pieces.Attributes;
using UnityEngine;
using static Chess.Boards.BoardUtil;

namespace Chess.GameSystems.MoveSystem {
    [Serializable]
    public class SimpleChessMoveSystem : ChessMoveSystemBase {
        
        public override List<BoardVector> getMovablePos(GameInfo info, BoardVector piecePos) {
            var board = info.Board;
            
            if(!isInScale(piecePos, board))
                throw new ArgumentException();

            var piece = board.getPiece(piecePos);
            if(piece == null)
                return new List<BoardVector>();
            
            var moveInfos = piece.getMoveInfos();
            var movable = new List<BoardVector>();
            var id = piece.getOwner().getId();
            var frontDir = info.getPlayer(piece.getOwner().getId()).FrontDirection;
            
            if (piece.hasAttribute<PawnAttribute>()) {
                var pawnAttr = piece.getAttribute<PawnAttribute>();
                for (int i = -1; i <= 1; i += 2) {
                    checkEnemy(i, 1, id, piecePos, info, movable);
                }

                if (pawnAttr.canMoveTwice(piecePos)) {
                    var movePos = piecePos + info.getPlayer(id).FrontDirection * 2;
                    var canPass = canPassFrom(id, piecePos, movePos, board);
                    if (canPass && board.getStatus(id, movePos) == SquareStatus.FREE) {
                        movable.Add(movePos);
                    }
                }

                foreach (var moveInfo in moveInfos) {
                    var movablePoses = getInfoPos(moveInfo, frontDir, piecePos, board, id);
                    foreach (var pos in movablePoses) {
                        if (board.getStatus(id, pos) == SquareStatus.FREE) {
                            movable.Add(pos);
                        }
                    }
                }
                
            } else {
                foreach (var moveInfo in moveInfos) {
                    movable.AddRange(getInfoPos(moveInfo, frontDir, piecePos, board, id));
                }
            }

            return movable;
        }

        void checkEnemy(int deltaX, int deltaY, int id, BoardVector piecePos, GameInfo info, List<BoardVector> vectors) {
            var dir = new BoardVector(deltaX, deltaY);
            var plDir = BoardVector.rotateTo(dir, info.getPlayer(id).FrontDirection);
            if (info.Board.getStatus(id, piecePos + plDir) == SquareStatus.ENEMY) {
                vectors.Add(piecePos + plDir);
            }
        }
    }
}
