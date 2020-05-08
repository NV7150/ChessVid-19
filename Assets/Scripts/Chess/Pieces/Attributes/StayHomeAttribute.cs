using System;
using System.Collections.Generic;
using Chess.Boards;
using Chess.GameSystems;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class StayHomeAttribute : PieceAttribute {
        private bool _isStayHome = false;
        private readonly CoVidAttribute _coVid;
        private CoVidPlayer _player;
        private bool _usingBed = false;
        private int _healCount = 0;
        private int HEAL_INTERVAL = 2;

        public bool IsStayHome => _isStayHome;

        public StayHomeAttribute(CoVidAttribute attribute, CoVidPlayer player) {
            _coVid = attribute;
            _player = player;
        }
        
        public string getAttributeName() {
            return "StayHome";
        }

        public Type getAttributeType() {
            return GetType();
        }

        public void updateTurn() {
            if (_isStayHome) {
                _healCount++;
                if (_healCount % HEAL_INTERVAL == 0) {
                    _coVid.disProgressStage();
                }
            } else {
                _healCount = 0;
            }
        }

        public bool stayHome() {
            if (_coVid.getStage() > 2) {
                if (_player.Beds <= 0)
                    return false;
                _player.Beds--;
                _usingBed = true;
            } else {
                _usingBed = false;
            }

            _player.StayingHomeNum++;
            _isStayHome = true;
            _coVid.addAntiStatus(AntiStatus.ANTI_INFLUENCE);
            _coVid.addAntiStatus(AntiStatus.ANTI_PROGRESS);
            _coVid.addAntiStatus(AntiStatus.ANTI_HAVING);
            return true;
        }

        public void outHome() {
            if (_usingBed)
                _player.Beds++;
            
            _player.StayingHomeNum--;
            _isStayHome = false;
            _coVid.removeAntiStatus(AntiStatus.ANTI_INFLUENCE);
            _coVid.removeAntiStatus(AntiStatus.ANTI_PROGRESS);
            _coVid.removeAntiStatus(AntiStatus.ANTI_HAVING);
        }

        public List<MoveInfo> getStayHomeMoveInfo() {
            return new List<MoveInfo>();
        }
    }
}
