using System;
using UnityEngine;

namespace Chess.Boards {
    [Serializable]
    public struct BoardVector {
        [SerializeField]public int x;
        [SerializeField]public int y;
        public static readonly BoardVector ZERO = new BoardVector(0,0);
        public BoardVector(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public BoardVector(float x, float y) {
            this.x = (int) x;
            this.y = (int) y;
        }
        
        public static BoardVector operator +(BoardVector vec) 
            => vec;
        public static BoardVector operator -(BoardVector vec) 
            => new BoardVector(-vec.x, -vec.y);
        public static BoardVector operator +(BoardVector vec1, BoardVector vec2) 
            => new BoardVector(vec1.x + vec2.x, vec1.y + vec2.y);
        public static BoardVector operator -(BoardVector vec1, BoardVector vec2)
            => new BoardVector(vec1.x - vec2.x, vec1.y - vec2.y);
        
        public static BoardVector operator *(BoardVector vec, int scale)
            => new BoardVector(vec.x * scale, vec.y * scale);
        
        public static bool operator ==(BoardVector vec1, BoardVector vec2)
            => vec1.x == vec2.x && vec1.y == vec2.y;

        public static bool operator !=(BoardVector vec1, BoardVector vec2)
            => vec1.x != vec2.x || vec1.y != vec2.y;
        
        //UnityのVector2を使って無理矢理回転させる
        public static BoardVector rotateTo(BoardVector vec, BoardVector direction) {
            var vec2 = new Vector2(vec.x, vec.y);
            var direction2 = new Vector2(direction.x, direction.y);
            var rotatedVec2 = Quaternion.FromToRotation(new Vector2(0,1), direction2) * vec2;
            return new BoardVector(Mathf.Round(rotatedVec2.x), Mathf.Round(rotatedVec2.y));
        }
        
        public bool Equals(BoardVector other) {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj) {
            return obj is BoardVector other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (x * 397) ^ y;
            }
        }

        public override string ToString() {
            return "BoardVec:(" + x + "," + y + ")";
        }
    }
}