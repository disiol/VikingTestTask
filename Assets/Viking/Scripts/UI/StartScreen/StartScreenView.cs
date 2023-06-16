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
            // TODO: Perform the desired action to exit the application
            Debug.Log("Exit button clicked");
            Application.Quit();
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