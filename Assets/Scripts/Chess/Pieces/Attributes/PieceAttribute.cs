using System;

namespace Chess.Pieces.Attributes {
    public interface PieceAttribute {
        string getAttributeName();
        Type getAttributeType();
    }

    public static class PieceAttributeUtil {
        public static T castAttribute<T>(PieceAttribute attribute) {
            if (attribute.getAttributeType() is T) {
                return (T)attribute;
            }
            throw new ArgumentException("Except Attribute");
        }
    }
}
