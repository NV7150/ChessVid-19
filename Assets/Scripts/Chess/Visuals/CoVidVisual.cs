using System.Collections.Generic;
using Chess.Pieces;
using Chess.Pieces.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Visuals {
    public class CoVidVisual : MonoBehaviour {
        [SerializeField] private PieceBase piece;
        [SerializeField] private Image stayHomeVisual;
        [SerializeField] private Image coVidVisual;
        [SerializeField] private List<Color> coVidColor;
        private CoVidAttribute _coVidAttribute;
        private StayHomeAttribute _stayHomeAttribute;
        private int _prevStage = -1;
        private bool _prevStayHome = false;
        private bool _initialized = false;

        // Start is called before the first frame update
        void Start() {
            initAttr();
        }

        void initAttr() {
            int inited = 0;
            
            if (piece.hasAttribute<CoVidAttribute>()) {
                _coVidAttribute = piece.getAttribute<CoVidAttribute>();
                inited++;
            }

            if (piece.hasAttribute<StayHomeAttribute>()) {
                _stayHomeAttribute = piece.getAttribute<StayHomeAttribute>();
                inited++;
            }

            if (inited >= 2) {
                _initialized = true;
            }
        }
        
        

        // Update is called once per frame
        void Update() {
            if (_initialized) {
                changeCoVidImage();
                changeStayHomeImage();
            } else {
                initAttr();
            }
        }

        void changeCoVidImage() {
            var stage = _coVidAttribute.getStage();
            if (stage == _prevStage)
                return;
            if (stage == 0) {
                coVidVisual.gameObject.SetActive(false);
                _prevStage = stage;
                return;
            }

            coVidVisual.gameObject.SetActive(true);
            coVidVisual.color = coVidColor[stage - 1];
            _prevStage = stage;
        }
        

        void changeStayHomeImage() {
            var isStayHome = _stayHomeAttribute.IsStayHome;
            if (isStayHome == _prevStayHome)
                return;
            
            stayHomeVisual.gameObject.SetActive(isStayHome);
            _prevStayHome = isStayHome;
        }
    }
}
