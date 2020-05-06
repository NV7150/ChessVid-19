using UnityEngine;
using UnityEngine.UI;

namespace Chess.Visuals {
    public class ResultVisual : MonoBehaviour {
        [SerializeField] private Text text;

        public static int WinnerId { get; set; }

        // Start is called before the first frame update
        void Start() {
            text.text = "Player " + WinnerId + " wins!";
        }

        // Update is called once per frame
        void Update() {
        
        }
    }
}
