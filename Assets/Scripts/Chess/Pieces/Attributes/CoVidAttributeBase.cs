using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;
using static Chess.Pieces.Attributes.AntiStatus;
using Random = UnityEngine.Random;

namespace Chess.Pieces.Attributes {
    public abstract class CoVidAttributeBase : CoVidAttribute {
        private int _stage;
        private int _influencedTurn = -1;
        private bool _isDead = false;
        private readonly Dictionary<AntiStatus, bool> _currAntiStatus = new Dictionary<AntiStatus, bool>();
        private int _progressCount = 0;
        private static readonly int PROGRESS_INTERVAL = 2;

        protected int Stage {
            get => _stage;
            set => _stage = value;
        }

        protected int InfluencedTurn => _influencedTurn;

        protected bool IsDead {
            get => _isDead;
            set => _isDead = value;
        }

        protected bool getAntiStatus(AntiStatus status) {
            return _currAntiStatus[status];
        }

        public CoVidAttributeBase() {
            _currAntiStatus.Add(ANTI_HAVING, false);
            _currAntiStatus.Add(ANTI_INFLUENCE, false);
            _currAntiStatus.Add(ANTI_PROGRESS, false);
        }
        

        public int getStage() {
            return _stage;
        }

        public void influenced(int turn) {
            if (_currAntiStatus[ANTI_HAVING])
                return;
            
            if (_stage > 0)
                return;
            _influencedTurn = turn;
            _stage = 1;
        }

        public void progressStage() {
            if (_currAntiStatus[ANTI_PROGRESS] || _stage <= 0)
                return;
            _progressCount++;
            if (_progressCount % PROGRESS_INTERVAL == 0) {
                _stage++;
                if (_stage > 3)
                    _stage = 3;
            }
        }

        public void disProgressStage() {
            _stage--;
            if (_stage < 0)
                _stage = 0;
        }

        public virtual bool useModifiedMoveInfo() {
            return _stage >= 2;
        }
        
        public virtual void updateTurn(int turn) {
            if (turn != _influencedTurn) {
                progressStage();
            }
        }

        public void addAntiStatus(AntiStatus status){
            if(!_currAntiStatus.ContainsKey(status))
                throw new NotSupportedException("unsupported status");
            _currAntiStatus[status] = true;
        }

        public void removeAntiStatus(AntiStatus status) {
            _currAntiStatus[status] = false;
        }

        public bool isDeadByCoVid() {
            return _isDead;
        }

        protected bool isInfluenceProtected() {
            return _currAntiStatus[ANTI_INFLUENCE];
        }

        public virtual InfluenceInfo getInfluenceInfo(int turn) {

            bool isInfluence() {
                if (_influencedTurn == turn || _currAntiStatus[ANTI_INFLUENCE])
                    return false;
                var infectivity = getInfectivity();
                var rand = Random.Range(0, 100) + 1;
                return rand < infectivity; 
            }

            var area = getInfluenceArea();
            
            return new InfluenceInfo(isInfluence, area);
        }

        protected virtual List<BoardVector> getInfluenceArea() {
            var influenceArea = new List<BoardVector>();
            switch (getStage()) {
                case 3:
                    for (int i = 0; i <= 1; i++) {
                        for (int front = -2; front <= 2; front += 4) {
                            var x = (i == 0) ? front : 0;
                            var y = (i != 0) ? front : 0;
                            influenceArea.Add(new BoardVector(x, y));
                        }
                    }
                    goto case 2;
                    
                case 2:
                    for (int x = -1; x <= 1; x += 2) {
                        for (int y = -1; y <= 1; y += 2) {
                            influenceArea.Add(new BoardVector(x, y));
                        }
                    }
                    goto case 1;
                
                case 1:
                    for (int i = 0; i <= 1; i++) {
                        for (int front = -1; front <= 1; front += 2) {
                            var x = (i == 0) ? front : 0;
                            var y = (i != 0) ? front : 0;
                            influenceArea.Add(new BoardVector(x, y));
                        }
                    }

                    break;
            }

            return influenceArea;
        }

        protected virtual float getInfectivity() {
            switch (getStage()) {
                case 1:
                    return 10f;
                case 2:
                    return 30f;
                case 3:
                    return 50f;
            }

            return 0;
        }

        public abstract string getAttributeName();
        public abstract Type getAttributeType();
        public abstract List<MoveInfo> getModifiedMoveInfo();
    }
}
