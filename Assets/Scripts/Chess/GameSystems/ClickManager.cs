using System;
using Chess.Boards;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.GameSystems {
    public class ClickManager : MonoBehaviour {
        [SerializeField] private ChessManagerBase chessMan;
        [SerializeField] private GameInfo info;
        
        
        // Start is called before the first frame update
        void Start() {
        }

        // Update is called once per frame
        void Update() {
            mouseClick();
        }

        private void mouseClick() {
            if (Input.GetMouseButtonDown(0)) {
                Vector2 pos = Input.mousePosition;
                var board = info.Board;
                var boardS = board.getStartScreenPos();
                var boardE = boardS + board.getBoardRealScale();

                if (boardS.x <= pos.x && pos.x <= boardE.x && boardS.y <= pos.y && pos.y <= boardE.y) {
                    var difPos = pos - boardS;
                    var boardVecX = difPos.x /  (board.getSquareScale().x * info.Board.getScreenScaleRate());
                    var boardVecY = difPos.y / (board.getSquareScale().y * info.Board.getScreenScaleRate());

                    BoardVector vec = new BoardVector(boardVecX, boardVecY);
                    chessMan.placeSelected(vec);
                }
            }
        }

        (bool, bool) isFlip(Vector2 start, Vector2 end) {
            (bool, bool) returnTuple = (x: false, y: false);
            return (start.x < end.x, start.y < end.y);
        }

        int flip(int value, bool isX) {
            var size = info.Board.getBoardScale();
            var length = (isX) ? size.x - value - 1 : size.y - value - 1;
            return length;
        }
    }
}
