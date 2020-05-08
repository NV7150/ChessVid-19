using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.GameSystems.MoveSystem;
using Chess.Visuals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chess.GameSystems {
    enum ScmMode {
        SHOW_MOVE_POINT,
        SELECT_MOVE_POINT
    }
    
    [Serializable]
    public class SimpleChessManager : ChessManagerBase {
        [SerializeField] private ChessMoveSystemBase chessMoveSystem;
        [SerializeField] private GameInfo info;
        [SerializeField] private BoardVisual boardVisual;
        [SerializeField] private string resultSceneName;

        private int _turn = 0;

        private GamePlayer _gamePlayer;

        private ScmMode _currentMode = ScmMode.SHOW_MOVE_POINT;
        private List<BoardVector> _currentMovePoints;
        private BoardVector _selectingPos;

        private bool _staleMate = false;

        public GameInfo Info => info;

        protected BoardVisual BoardVisual => boardVisual;

        public override void placeSelected(BoardVector pos) {
            if (_gamePlayer == null)
                _gamePlayer = info.getFirstPlayer();
            
            switch (_currentMode) {
                case ScmMode.SHOW_MOVE_POINT:
                    checkStaleMate();
                    if(showMovePoint(pos)) 
                        _currentMode = ScmMode.SELECT_MOVE_POINT;
                    break;
                case ScmMode.SELECT_MOVE_POINT:
                    if (selectMovePoint(pos)) {
                        _currentMode = ScmMode.SHOW_MOVE_POINT;
                        updateTurn();
                    } 

                    break;
            }
        }
        
        protected void checkStaleMate() {
            _staleMate = true;
            var posFuncs = new List<BoardUtil.PosFunc>(){checkPos};
            BoardUtil.functionToEachPos(posFuncs, Info.Board);
            if (_staleMate) {
                endGame(true);
            }
        }

        bool checkPos(BoardVector pos) {
            var piece = Info.Board.getPiece(pos);
            if (piece == null || piece.getOwner().getId() != getCurrentPlayer().getId()) {
                return false;
            }

            if (chessMoveSystem.getMovablePos(info, pos).Count > 0) {
                _staleMate = false;
                return true;
            }

            return false;
        }

        protected bool showMovePoint(BoardVector pos) {
            var board = info.Board;
            var piece = board.getPiece(pos);

            if (piece == null || piece.getOwner().getId() != _gamePlayer.getId()) {
                return false;
            }

            var movable = chessMoveSystem.getMovablePos(info, pos);
            if (movable.Count == 0) {
                return false;
            }

            _currentMovePoints = movable;
            _selectingPos = pos;
            
            boardVisual.reset();
            boardVisual.highLightAll(_currentMovePoints);
            return true;
        }

        protected bool selectMovePoint(BoardVector pos) {
            if (!_currentMovePoints.Contains(pos))
                return false;
            
            info.Board.movePiece(_selectingPos, pos);
            cancelMove();
            return true;
        }

        public virtual void cancelMove() {
            if (_currentMode == ScmMode.SHOW_MOVE_POINT)
                return;
            
            _currentMode = ScmMode.SHOW_MOVE_POINT;
            boardVisual.reset();
        }

        public override void updateTurn() {
            info.checkLoser();
            if (info.getRemainPlayerNum() <= 1) {
                endGame();
            } else {
                bool turnEnd = false;
                _gamePlayer = info.getNextPlayer(ref turnEnd);
                if (turnEnd) {
                    _turn++;
                    turnEnded();
                }
            }
        }

        protected virtual void turnEnded() {
            
        }

        protected void endGame(bool staleMate = false) {
            bool dummy = false;
            var winner = info.getNextPlayer(ref dummy);
            if (winner != null && !staleMate) {
                ResultVisual.WinnerId = winner.getId();
                ResultVisual.Draw = false;
            } else {
                ResultVisual.Draw = true;
            }
            SceneManager.LoadScene(resultSceneName);
        }

        public override int getTurn() {
            return _turn;
        }

        public override ChessPlayer getCurrentPlayer() {
            if(_gamePlayer == null)
                _gamePlayer = info.getFirstPlayer();
            return _gamePlayer;
        }
    }
}
