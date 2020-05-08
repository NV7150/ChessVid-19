using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.Pieces;
using Chess.Pieces.Attributes;
using Chess.Visuals;
using UnityEngine;
using static Chess.GameSystems.ChessVidMode;
using Random = UnityEngine.Random;

namespace Chess.GameSystems {
    public enum ChessVidMode{
        SHOW_PLACE,
        SELECT_PLACE,
        STAY_HOME
    }
    
    public class ChessVidManager : SimpleChessManager {
        [SerializeField] private int defaultActionPoint = 3;
        [SerializeField] private int intervalInfluence = 2;
        private int _actionPoint;
        private ChessVidMode _currentChessVidMode = SHOW_PLACE;
        
        private readonly Dictionary<int, List<BoardVector>> _unInfluencedPoses = new Dictionary<int, List<BoardVector>>();

        public ChessVidMode CurrentChessVidMode => _currentChessVidMode;

        private void Awake() {
            _actionPoint = defaultActionPoint;
            for (int i = 0; i < Info.PlayerCount; i++) {
                _unInfluencedPoses.Add(Info.getPlayerFromIndex(i).getId(), new List<BoardVector>());
            }

        }

        private void Start() {
        }

        public override void placeSelected(BoardVector pos) {
            switch (_currentChessVidMode) {
                case SHOW_PLACE:
                    checkStaleMate();
                    if (showMovePoint(pos)) {
                        _currentChessVidMode = SELECT_PLACE;
                    }
                    break;
                
                case SELECT_PLACE:
                    if (selectMovePoint(pos)) {
                        _actionPoint -= 2;
                        checkNext();
                    }
                    break;
                
                case STAY_HOME:
                    if (stayHome(pos)) {
                        _actionPoint--;
                        checkNext();
                    }
                    break;
            }
        }

        void checkNext() {
            if (_actionPoint <= 0) {
                updateTurn();
                _actionPoint = defaultActionPoint;
                _currentChessVidMode = SHOW_PLACE;
                return;
            }

            if (_actionPoint >= 2) {
                _currentChessVidMode = SHOW_PLACE;
                return;
            }

            _currentChessVidMode = STAY_HOME;
        }

        public bool stayHome(BoardVector pos) {
            var piece = Info.Board.getPiece(pos);
            if (piece == null || piece.getOwner().getId() != getCurrentPlayer().getId())
                return false;
            
            if (piece.hasAttribute<StayHomeAttribute>()) {
                var stayHome = piece.getAttribute<StayHomeAttribute>();
                if (stayHome.IsStayHome) {
                    stayHome.outHome();
                } else {
                    stayHome.stayHome();
                }

                return true;
            }

            return false;
        }

        public override void cancelMove() {
            if (_currentChessVidMode == SHOW_PLACE || _currentChessVidMode ==  STAY_HOME)
                return;
            base.cancelMove();
            BoardVisual.reset();
            _currentChessVidMode = SHOW_PLACE;
        }

        public void changeMode() {
            if (_actionPoint < defaultActionPoint)
                return;
            
            if (_currentChessVidMode == SHOW_PLACE) {
                _currentChessVidMode = STAY_HOME;
            }else if (_currentChessVidMode == STAY_HOME) {
                _currentChessVidMode = SHOW_PLACE;
            }
        }

        public void passStayHome() {
            if (_currentChessVidMode == STAY_HOME && _actionPoint <= 1) {
                updateTurn();
                _actionPoint = defaultActionPoint;
                _currentChessVidMode = SHOW_PLACE;
            }
        }

        protected override void turnEnded() {
            if (getTurn() % intervalInfluence == 0) {
                foreach (var id in _unInfluencedPoses.Keys) {
                    var player = Info.getPlayer(id);
                    if (player.isLoser()) 
                        continue;

                    _unInfluencedPoses[id].Clear();
                }
            }
            
            BoardUtil.functionToEachPos(new List<BoardUtil.PosFunc> {placeProcess}, Info.Board);
            
            if (getTurn() % intervalInfluence == 0) {
                foreach (var id in _unInfluencedPoses.Keys) {
                    if (_unInfluencedPoses[id].Count > 0) {
                        var rand = Random.Range(0, _unInfluencedPoses[id].Count);
                        Info.Board.getPiece(_unInfluencedPoses[id][rand]).getAttribute<CoVidAttribute>().influenced(getTurn());
                    }
                }
            }
            
            Info.checkLoser();
            if (Info.getRemainPlayerNum() <= 1) {
                endGame();
            }
        }

        bool placeProcess(BoardVector pos) {
            var piece = Info.Board.getPiece(pos);
            if (piece == null)
                return false;
            
            if (piece.hasAttribute<CoVidAttribute>() && piece.hasAttribute<StayHomeAttribute>()) {
                CoVidAttributeUtil.influenceCheck(Info.Board, pos, getTurn());
                
                var attr = piece.getAttribute<CoVidAttribute>();
                attr.updateTurn(getTurn());
                
                if (attr.isDeadByCoVid()) {
                    Info.Board.takePiece(pos);
                }

                var stayHome = piece.getAttribute<StayHomeAttribute>();
                
                var id = piece.getOwner().getId();
                if (attr.getStage() == 0 && !stayHome.IsStayHome) {
                    _unInfluencedPoses[id].Add(pos);
                }

                stayHome.updateTurn();
            }

            return false;
        }
    }
}
