using System;
using System.Collections.Generic;
using Chess.GameSystems;
using Chess.Pieces;
using Chess.Pieces.Attributes;
using UnityEngine;

namespace Chess.Boards {
    public class CoVidListBoardInitializer : ListBoardInitializer {
        [SerializeField] private List<CoVidPlayerInspectInfo> coVidPlayers;
        private Dictionary<int, CoVidPlayerInspectInfo> _inspectInfos;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        protected override Piece initPiece(PieceLoadInfo loadInfo, int x, int y) {
            if (_inspectInfos == null) {
                _inspectInfos = new Dictionary<int, CoVidPlayerInspectInfo>();
                foreach (var playerInfo in coVidPlayers) {
                    _inspectInfos.Add(playerInfo.Id, playerInfo);
                }
            }

            var piece = base.initPiece(loadInfo, x, y);
            
            CoVidAttribute coVidAttribute = null;
            switch (loadInfo.PieceName) {
                case "pawn":
                    coVidAttribute = new PawnCoVidAttribute();
                    break;
                case "knight":
                    coVidAttribute = new KnightCoVidAttribute();
                    break;
                case "luke":
                    coVidAttribute = new LukeCoVidAttribute();
                    break;
                case "bishop":
                    coVidAttribute = new BishopCoVidAttribute();
                    break;
                case "queen":
                    coVidAttribute = new QueenCoVidAttribute();
                    break;
                case "king":
                    coVidAttribute = new KingCoVidAttribute();
                    break;
            }

            if (coVidAttribute != null) {
                piece.addAttribute(coVidAttribute);
                var coVidPlayer = _inspectInfos[loadInfo.Player.getId()].Player;
                piece.addAttribute(new StayHomeAttribute(coVidAttribute, coVidPlayer));
            }


            return piece;
        }
    }
    [Serializable]
    public class CoVidPlayerInspectInfo {
        [SerializeField] private int id;
        [SerializeField] private CoVidPlayer player;

        public int Id => id;

        public CoVidPlayer Player => player;
    }
}
