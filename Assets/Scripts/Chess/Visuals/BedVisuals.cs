using Chess.GameSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Visuals {
    public class BedVisuals : MonoBehaviour {
        [SerializeField] private GamePlayer player;
        [SerializeField] private CoVidPlayer cPlayer;
        [SerializeField] private Text currText;

        private int _prevBed = 0;

        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
            updateText();
        }

        void updateText() {
            if (cPlayer.Beds != _prevBed) {
                currText.text = "BEDS\n" + cPlayer.Beds;
                _prevBed = cPlayer.Beds;
            }
        }
    }
}
