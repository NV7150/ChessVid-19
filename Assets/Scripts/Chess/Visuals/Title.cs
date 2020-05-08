using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chess.Visuals {
    public class Title : MonoBehaviour {
        [SerializeField] private string loadSceneName;
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        }

        public void loadScene() {
            ResultVisual.Draw = false;
            ResultVisual.WinnerId = -1;
            SceneManager.LoadScene(loadSceneName);
        }
    }
}
