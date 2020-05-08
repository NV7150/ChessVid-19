using Chess.GameSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Visuals {
    public class ModeVisual : MonoBehaviour {
        [SerializeField] private ChessVidManager chessVidMan;
        [SerializeField] private Text text;

        private ChessVidMode _prevMode = ChessVidMode.SELECT_PLACE;
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
            updateText();
        }

        void updateText() {
            var mode = chessVidMan.CurrentChessVidMode;
            if (_prevMode != mode) {
                string str = "";
                switch (mode) {
                    case ChessVidMode.SHOW_PLACE:
                        str = "Select Piece";
                        break;
                    case ChessVidMode.SELECT_PLACE:
                        str = "Select Place";
                        break;
                    case ChessVidMode.STAY_HOME:
                        str = "Stay Home";
                        break;
                }

                text.text = str;
            } 
        }
    }
}
