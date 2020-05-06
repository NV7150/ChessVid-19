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

        public override void placeSelected(BoardVector pos) {
            if (_gamePlayer == null)
                _gamePlayer = info.getFirstPlayer();
            
            switch (_currentMode) {
                case ScmMode.SHOW_MOVE_POINT:
                    showMovePoint(pos);
                    break;
                case ScmMode.SELECT_MOVE_POINT:
                    selectMovePoint(pos);
                    break;
            }
        }

        void showMovePoint(BoardVector pos) {
            var board = info.Board;
            var piece = board.getPiece(pos);

            if (piece == null || piece.getOwner().getId() != _gamePlayer.getId()) {
                return;
            }

            var movable = chessMoveSystem.getMovablePos(info, pos);
            if (movable.Count == 0) {
                return;
            }

            _currentMode = ScmMode.SELECT_MOVE_POINT;
            _currentMovePoints = movable;
            _selectingPos = pos;
            
            boardVisual.reset();
            boardVisual.highLightAll(_currentMovePoints);
        }

        void selectMovePoint(BoardVector pos) {
            if (!_currentMovePoints.Contains(pos))
                return;
            
            info.Board.movePiece(_selectingPos, pos);
            cancelMove();
            updateTurn();
        }

        public void cancelMove() {
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
                }
            }
        }

        void endGame() {
            bool dummy = false;
            var winner = info.getNextPlayer(ref dummy);
            ResultVisual.WinnerId = winner.getId();
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
