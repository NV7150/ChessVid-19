using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chess.Boards;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Visuals {
    public class BoardVisual : MonoBehaviour {
        [SerializeField] private string matchingStr = @"([+-]?[0-9]+\.?[0-9]*)";
        [SerializeField] private int numOfEdge = 8;
        [SerializeField] private Color highLightColor;
        private List<List<Image>> _boards = new List<List<Image>>();
        private List<List<Color>> _colors = new List<List<Color>>();

        private void Awake() {
            initBoards();
        }

        void initBoards() {
            var children = new List<Image>();
            for (int i = 0; i < transform.childCount; i++) {
                children.Add(transform.GetChild(i).GetComponent<Image>());
            }
            children.Sort((a, b) => getOrder(a.name) - getOrder(b.name));
            int index = 0;
            foreach (var child in children) {
                if(child.transform.parent != transform)
                    continue;
                if (index % numOfEdge == 0) {
                    _boards.Add(new List<Image>());
                    _colors.Add(new List<Color>());
                }
                _boards[index / numOfEdge].Add(child);
                _colors[index / numOfEdge].Add(child.color);
                index++;
            }

            _boards = BoardUtil.switchXY(BoardUtil.flipY(_boards));
            _colors = BoardUtil.switchXY(BoardUtil.flipY(_colors));
        }


        int getOrder(string name) {
            if (Regex.IsMatch(name, matchingStr)) {
                var matchStr = Regex.Match(name, matchingStr).Value;
                return int.Parse(matchStr);
            }
            return 0;
        }
    
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public void highLightAll(List<BoardVector> poses) {
            foreach (var pos in poses) {
                highLight(pos);
            }
        }
        
        public void highLight(BoardVector pos) {
            _boards[pos.x][pos.y].color = highLightColor;
        }

        public void unHighLight(BoardVector pos) {
            _boards[pos.x][pos.y].color = _colors[pos.x][pos.y];
        }

        public void reset() {
            int x = 0, y = 0;
            foreach (var boardRow in _boards) {
                foreach (var square in boardRow) {
                    square.color = _colors[x][y];
                    y++;
                }
                x++;
                y = 0;
            }
        }
    }
}
