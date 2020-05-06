using Chess.GameSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Visuals {
    public class TurnPlayerShower : MonoBehaviour {
        [SerializeField] private Image showImage;
        [SerializeField] private Text showText;
        [SerializeField] private ChessManagerBase chessMan;
        [SerializeField] private GameInfo info;
        private int _prevId = -1;
        
        // Start is called before the first frame update
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {
            var id = chessMan.getCurrentPlayer().getId();
            if (id != _prevId) {
                var player = info.getPlayer(chessMan.getCurrentPlayer().getId());
                showImage.color = player.PlayerColor;
                showText.color = flipColor(player.PlayerColor);
                
                showText.text = player.getId() + "";
                _prevId = id;
            }
        }

        Color flipColor(Color col) {
            return new Color( 1.0f - col.r, 1.0f- col.g, 1.0f - col.b);
        }
    }
}
