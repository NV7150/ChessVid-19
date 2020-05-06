using Chess.Pieces;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Boards {
    public abstract class BoardBase : MonoBehaviour, Board {
        [SerializeField] private CanvasScaler scaler;

        private float _canvasScale = -1;
        
        void initCanvasScale() {
            var referenceVal = getReferVal(scaler.matchWidthOrHeight, scaler.referenceResolution);
            var currVal = getReferVal(scaler.matchWidthOrHeight, new Vector2(Screen.width, Screen.height));
            _canvasScale = currVal / referenceVal;
        }

        float getReferVal(float val, Vector2 vec) {
            return (val >= 0.5) ? vec.y : vec.x;
        }
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public GameObject getGameObject() {
            return gameObject;
        }

        public abstract Vector2 getStartScreenPos();

        public abstract BoardVector getBoardScale();

        public abstract Vector2 getBoardRealScale();

        public abstract Vector2 getSquareScale();

        public float getScreenScaleRate() {
            if(_canvasScale < 0)
                initCanvasScale();
            return _canvasScale;
        }

        public abstract Piece getPiece(BoardVector pos);

        public abstract void movePiece(BoardVector prevPos, BoardVector newPos);

        public abstract void takePiece(BoardVector piecePos);

        public abstract void updateTurn();

        public abstract SquareStatus getStatus(int id, BoardVector pos);
    }
}
