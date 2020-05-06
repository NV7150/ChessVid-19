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
            
            foreach (var moveInfo in moveInfos) {
                var dir = BoardVector.rotateTo(moveInfo.Direction, info.getPlayer(id).FrontDirection);

                if (moveInfo.IsEndless) {
                    var goingPos = searchForEndless(id, piecePos, dir, board, moveInfo.IsJump);
                    movable.AddRange(goingPos);
                } else {
                    if(canPassFrom(id, piecePos, piecePos + dir, board, moveInfo.IsJump))
                        movable.Add(piecePos + dir);
                }
            }

            if (piece.hasAttribute<PawnAttribute>()) {
                var pawnAttr = piece.getAttribute<PawnAttribute>();
                for (int i = -1; i <= 1; i += 2) {
                    checkEnemy(i, 1, id, piecePos, info, movable);
                }

                if (pawnAttr.canMoveTwice(piecePos)) {
                    var movePos = piecePos + info.getPlayer(id).FrontDirection * 2;
                    if (board.getStatus(id, movePos) == SquareStatus.FREE) {
                        movable.Add(movePos);
                    }
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
