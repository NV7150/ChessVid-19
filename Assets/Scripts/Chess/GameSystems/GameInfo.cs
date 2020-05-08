using System;
using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.GameSystems {
    public class GameInfo : MonoBehaviour {
        [SerializeField] private BoardBase board;
        [SerializeField] private List<GamePlayer> gamePlayers;
        private Dictionary<int, GamePlayer> _idPlayerDic = new Dictionary<int, GamePlayer>();

        public Board Board => board;
        public int PlayerCount => gamePlayers.Count;

        private int _index = 0;
        private List<int> _remainPlayers = new List<int>();
        
        // Start is called before the first frame update
        void Start() {
            foreach (var player in gamePlayers) {
                _idPlayerDic.Add(player.getId(), player);
                _remainPlayers.Add(player.getId());
            }

            _remainPlayers.Sort((a, b) => a - b);
        }

        // Update is called once per frame
        void Update() {
        
        }

        public GamePlayer getNextPlayer(ref bool isTurnEnded) {
            if (getRemainPlayerNum() < 1) {
                return null;
            }

            isTurnEnded = false;
            
            _index++;
            if (_index >= _remainPlayers.Count) {
                _index = 0;
                isTurnEnded = true;
            }

            return getPlayer(_remainPlayers[_index]);
        }

        public int getRemainPlayerNum() {
            return _remainPlayers.Count;
        }
        
        public void checkLoser() {
            var count = _remainPlayers.Count;
            var loseId = new List<int>();
            for (int i = 0; i < count; i++) {
                if (getPlayer(_remainPlayers[i]).isLoser()) {
                    loseId.Add(_remainPlayers[i]);
                }
            }

            foreach (var id in loseId) {
                _remainPlayers.Remove(id);
            }
        }
        
        public GamePlayer getPlayer(int id) {
            return _idPlayerDic[id];
        }

        public GamePlayer getPlayerFromIndex(int index) {
            return gamePlayers[index];
        }

        public GamePlayer getFirstPlayer() {
            return gamePlayers[0];
        }

        public void losePlayer(int id) {
            if(!_remainPlayers.Contains(id))
                throw new ArgumentException("id:" + id + " not found");
            _remainPlayers.Remove(_remainPlayers.IndexOf(id));
        }
    }
}
