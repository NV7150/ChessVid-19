using System;
using System.Collections.Generic;
using Chess.GameSystems;
using Chess.Pieces;
using Chess.Pieces.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Boards {
    public class ListBoardInitializer : MonoBehaviour {
        [SerializeField] private ListBoard listBoard;
        [SerializeField] private CsvLoader loader;
        [SerializeField] private List<PieceLoadInfo> loadInfos;
        [SerializeField] private Transform canvas;
        private readonly Dictionary<string, PieceLoadInfo> _loadInfoDic = new Dictionary<string, PieceLoadInfo>();


        private void Awake() {
            foreach (var loadInfo in loadInfos) {
                _loadInfoDic.Add(loadInfo.PieceSymbol, loadInfo);
            }
            loadStructure();
        }

        // Start is called before the first frame update
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        }

        void loadStructure() {
            loader.loadCsv();
            var pieceSymbols = BoardUtil.switchXY(BoardUtil.flipY(loader.LoadedCsv));
            var pieceList = new List<List<Piece>>();
            int x = 0;
            int y = 0;
            foreach (var symbolRow in pieceSymbols) {
                pieceList.Add(new List<Piece>());
                foreach (var symbol in symbolRow) {
                    if (!_loadInfoDic.ContainsKey(symbol)) {
                        pieceList[x].Add(null);
                        y++;
                        continue;
                    }

                    var loadInfo = _loadInfoDic[symbol];
                    var piece = initPiece(loadInfo, x, y);

                    if (piece.hasAttribute<PawnAttribute>()) {
                        piece.getAttribute<PawnAttribute>().setStartPos(new BoardVector(x, y));
                    }

                    pieceList[x].Add(piece);
                    y++;
                }

                y = 0;
                x++;
            }
            
            listBoard.initializeBoard(pieceList);
        }
        
        protected virtual Piece initPiece(PieceLoadInfo loadInfo, int x, int y){
            var obj = Instantiate(loadInfo.PiecePrefab, canvas, true);
            var piece = obj.GetComponent<PieceBase>();
            piece.initializePiece(loadInfo.Player);
            piece.moveTo(listBoard.getStartScreenPos(), listBoard.getSquareScale(), new BoardVector(x, y), 1);
            obj.GetComponent<Image>().color = loadInfo.Player.PlayerColor;
            loadInfo.Player.setPieceNum(loadInfo.Player.getPieceNum() + 1);

            if (piece.hasAttribute<PawnAttribute>()) {
                piece.getAttribute<PawnAttribute>().setStartPos(new BoardVector(x, y));
            }

            return piece;
        }
    }
    

    [Serializable]
    public class PieceLoadInfo {
        [SerializeField] private string pieceSymbol;
        [SerializeField] private string pieceName;
        [SerializeField] private GameObject piecePrefab;
        [SerializeField] private GamePlayer player;

        public string PieceSymbol => pieceSymbol;

        public GameObject PiecePrefab => piecePrefab;

        public GamePlayer Player => player;

        public string PieceName => pieceName;
    }
}
