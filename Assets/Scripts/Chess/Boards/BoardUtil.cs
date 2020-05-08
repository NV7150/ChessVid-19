using System;
using System.Collections.Generic;
using Chess.Pieces;
using UnityEngine;
using static Chess.Boards.SquareStatus;

namespace Chess.Boards {
    public static class BoardUtil {
        public static bool isInScale(BoardVector pos, BoardVector boardScale) {
            return 0 <= pos.x && pos.x < boardScale.x && 0 <= pos.y && pos.y < boardScale.y;
        }

        public static bool isInScale(BoardVector pos, Board board) {
            return isInScale(pos, board.getBoardScale());
        }

        public static bool canMoveTo(int id, BoardVector pos, Board board) {
            var status = board.getStatus(id, pos);
            return status == FREE || status == ENEMY;
        }

        public static bool canPassFrom(int id, BoardVector from, BoardVector to, Board board, bool canJump = false) {
            if (canJump)
                return canMoveTo(id, to, board);
            if (!canMoveTo(id, to, board))
                return false;
        
            var dir = to - from;
            var dirX = getDir(dir.x);
            var dirY = getDir(dir.y);
            BoardVector currPos = from;
            List<List<bool>> movableList = new List<List<bool>>();
            movableList.Add(new List<bool>());
            int y = 0;
            int x = 0;
        
            //開始地点を左下、目標到着地点を右上とした時の状況をmovableListに格納
            //あるマス(currPos)に対し、movableListの情報からそのマスに侵入する経路があるかどうかを取得
            //上の動作を左→右、上行って戻って左→右...とすることで経路を探索
            //O(x * y)で経路探索可能
            while (true) {
                bool canEnter = 
                    canEnterFrom(movableList, x - 1, y) ||
                    canEnterFrom(movableList, x, y - 1) ||
                    canEnterFrom(movableList, x - 1, y - 1);
            
                if (currPos == to) {
                    return canEnter && canMoveTo(id, currPos, board);
                }
            
                movableList[y].Add((board.getStatus(id, currPos) == FREE && canEnter) || (x == 0 && y == 0));
                
                if (isXEnteredFin(currPos, to, dirX)) {
                    movableList.Add(new List<bool>());
                    currPos.y += dirY;
                    currPos.x = from.x;
                    x = 0;
                    y++;
                } else {
                    currPos.x += dirX;
                    x++;
                }
                

                if (x > 64 || y > 64) {
                    Debug.Log( "curr:" + currPos);
                    Debug.Log("from : " + from + " to:" + to);
                    Debug.Log("dirX:" + dirX + " dirY:" + dirY);
                    throw new InvalidProgramException("infinity loop");
                }
            }
        }

        static bool isXEnteredFin(BoardVector currPos, BoardVector to, int dir) {
            switch (dir) {
                case 1:
                    return currPos.x >= to.x;
                case -1:
                    return currPos.x <= to.x;
                case 0:
                    return true;
            }

            throw new InvalidProgramException("invalid direction");
        }

        static bool canEnterFrom(List<List<bool>> list, int x, int y) {
            if (x < 0 || y < 0) {
                return false;
            }

            return list[y][x];
        }

        static int getDir(int dir) {
            if (dir == 0)
                return 0;
            return (dir > 0) ? 1 : -1;
        }

        public static List<BoardVector>  searchForEndless(int id, BoardVector from, BoardVector direction, Board board, bool canJump = false) {
            var pos = from;
            var movablePos = new List<BoardVector>();
            int looped = 0;
        
            while (true) {
                looped++;
                if(looped > 10)
                    throw new InvalidProgramException("infinity loop");
            
                if (canJump) {
                    if (canMoveTo(id, pos + direction, board)) {
                        pos += direction;
                        movablePos.Add(pos);
                        if (board.getStatus(id, pos) != ENEMY) 
                            continue;
                    } 
                    break;
                } else {
                    if (canPassFrom(id, pos, pos + direction, board)) {
                        pos += direction;
                        movablePos.Add(pos);
                        if (board.getStatus(id, pos) != ENEMY) 
                            continue;
                    }
                    break;
                }
            }

            return movablePos;
        }

        public static List<List<T>> switchXY<T>(List<List<T>> list) {
            var returnList = new List<List<T>>();
            int i = 0;
            foreach (var elemL in list) {
                returnList.Add(new List<T>());
                foreach (var elem in elemL) {
                    returnList[i].Add(elem);
                }

                i++;
            }

            i = 0;
            foreach (var elemL in list) {
                foreach (var elem in elemL) {
                    returnList[i % returnList.Count][i / returnList.Count] = elem;
                    i++;
                }
            }

            return returnList;
        }

        public static List<List<T>> flipY<T>(List<List<T>> list) {
            var flipped = new List<List<T>>();
            for (int i = list.Count - 1; i >= 0; i--) {
                flipped.Add(list[i]);
            }

            return flipped;
        }

        public static Vector2 flipScreenVec(Vector2 vec, bool isX) {
            if (isX) {
                vec.x = Screen.width - vec.x;
            } else {
                vec.y = Screen.height - vec.y;
            }

            return vec;
        }

        public delegate bool PosFunc(BoardVector pos);

        public static void functionToEachPos(List<PosFunc> functions, Board board) {
            var scale = board.getBoardScale();
            for (int x = 0; x < scale.x; x++) {
                for (int y = 0; y < scale.y; y++) {
                    foreach (var func in functions) {
                        if (func(new BoardVector(x, y)))
                            break;
                    }
                }
            }
        }

        public static List<BoardVector> getInfoPos(
            MoveInfo moveInfo, BoardVector frontDirection, BoardVector piecePos, Board board, int id) {
            
            var movable = new List<BoardVector>();
            var dir = BoardVector.rotateTo(moveInfo.Direction, frontDirection);

            if (moveInfo.IsEndless) {
                var goingPos = searchForEndless(id, piecePos, dir, board, moveInfo.IsJump);
                movable.AddRange(goingPos);
            } else {
                var currPos = piecePos;
                for (int i = 0; i < moveInfo.MoveAmounts; i++) {
                    if (canPassFrom(id, currPos, currPos + dir, board, moveInfo.IsJump)) {
                        movable.Add(currPos + dir);
                    }

                    currPos += dir;
                }
            }

            return movable;
        }
    }
}