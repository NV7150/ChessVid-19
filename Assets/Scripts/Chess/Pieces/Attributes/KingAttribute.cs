using System;

namespace Chess.Pieces.Attributes {
    public class KingAttribute : PieceAttribute {
        public string getAttributeName() {
            return "king";
        }

        public Type getAttributeType() {
            return GetType();
        }
    }
}