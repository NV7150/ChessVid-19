using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Chess.Visuals {
    public class ResultVisual : MonoBehaviour {
        [SerializeField] private Text text;
        [SerializeField] private string titleSceneName = "Title";

        public static int WinnerId { get; set; }
        public static bool Draw { get; set; }

        // Start is called before the first frame update
        void Start() {
            if (!Draw) {
                text.text = "Player " + WinnerId + " wins!";
            } else {
                text.text = "Draw!";
            }
        }

        // Update is called once per frame
        void Update() {
        
        }

        public void backToTitle() {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}
