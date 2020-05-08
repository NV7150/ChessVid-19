using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public enum AntiStatus {
        ANTI_INFLUENCE,
        ANTI_HAVING,
        ANTI_PROGRESS
    }
    
    public interface CoVidAttribute : PieceAttribute {
        int getStage();
        void influenced(int turn);
        void progressStage();
        void disProgressStage();
        bool useModifiedMoveInfo();
        List<MoveInfo> getModifiedMoveInfo();
        void updateTurn(int turn);
        void addAntiStatus(AntiStatus status);
        void removeAntiStatus(AntiStatus status);
        bool isDeadByCoVid();
        InfluenceInfo getInfluenceInfo(int turn);
    }

    public static class CoVidAttributeUtil {
        public static void influenceCheck(Board board, BoardVector pos, int turn) {

            var piece = board.getPiece(pos);
            if (piece == null || !piece.hasAttribute<CoVidAttribute>()) {
                return;
            }

            var coVid = piece.getAttribute<CoVidAttribute>();
            if (coVid.getStage() <= 0)
                return;

            var id = piece.getOwner().getId();
            var influenceInfo = coVid.getInfluenceInfo(turn);
            
            var influenceList = influenceInfo.getInfluenceDir();
            
            foreach (var influenceDir in influenceList) {
                if (influenceInfo.willInfluence()) {
                    var influencePos = pos + influenceDir;

                    if (board.getStatus(id, influencePos) == SquareStatus.ALLY) {

                        var influencePiece = board.getPiece(influencePos);

                        if (influencePiece.hasAttribute<CoVidAttribute>()) {
                            var attr = influencePiece.getAttribute<CoVidAttribute>();
                            attr.influenced(turn);
                        }
                    }
                }
            }
            
        }
    }
}
