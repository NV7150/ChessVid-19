using System.Collections.Generic;
using Chess.Boards;
using UnityEngine;

namespace Chess.Pieces.Attributes {
    public class InfluenceInfo {
        private readonly List<BoardVector> _influenceDir = new List<BoardVector>();

        public delegate bool InfluenceJudge();

        private readonly InfluenceJudge _influenceJudge;

        public InfluenceInfo(InfluenceJudge func, List<BoardVector> poses) {
            _influenceDir = poses;
            _influenceJudge = func;
        }
        
        public bool willInfluence() {
            var isInfluence = _influenceJudge();
            return isInfluence;
        }

        public List<BoardVector> getInfluenceDir() {
            return new List<BoardVector>(_influenceDir);
        }
    }
}
