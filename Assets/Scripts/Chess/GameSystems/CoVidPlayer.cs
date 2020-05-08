using System;
using UnityEngine;

namespace Chess.GameSystems {
    public class CoVidPlayer : MonoBehaviour {
        [SerializeField] private int defaultBed = 3;
        private int _beds;
        private int _stayingHomeNum;
        private GamePlayer _gamePlayer;

        public int Beds {
            get => _beds;
            set => _beds = value;
        }

        public int StayingHomeNum {
            get => _stayingHomeNum;
            set => _stayingHomeNum = value;
        }

        public int Id {
            get => _gamePlayer.getId();
        }

        private void Awake() {
            _beds = defaultBed;
            _gamePlayer = GetComponent<GamePlayer>();
        }

        // Start is called before the first frame update
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        
        }
    }
}
