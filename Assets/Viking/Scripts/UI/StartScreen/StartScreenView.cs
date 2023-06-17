using UnityEngine;
using UnityEngine.UI;

namespace Viking.Scripts.UI.StartScreen
{
    public class StartScreenView : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject game;
        private GameObject _gameObjectGame;

        private void Start()
        {
            CheckSerializeFieldToNull();
        }


        private void OnPlayButtonClick()
        {
            Debug.Log("Play button clicked");
            game.SetActive(true);
            this.gameObject.SetActive(false);
        }

        private void OnExitButtonClick()
        {
            #if UNITY_STANDALONE
                        //Quit the application
                        Application.Quit();
            #endif

                        //If we are running in the editor
            #if UNITY_EDITOR
                        //Stop playing the scene
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        private void CheckSerializeFieldToNull()
        {
            if (playButton == null)
            {
                Debug.LogWarning(
                    "StartScreenView Presenter needs a playButton sure one is set in The Inspector",
                    gameObject);
            }
            else
            {
                playButton.onClick.AddListener(OnPlayButtonClick);
            }

            if (exitButton == null)
            {
                Debug.LogWarning(
                    "StartScreenView Presenter needs a exitButton  make sure one is set in The Inspector",
                    gameObject);
            }
            else
            {
                exitButton.onClick.AddListener(OnExitButtonClick);
            }
        }
    }
}