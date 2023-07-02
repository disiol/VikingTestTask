using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Viking.Scripts.Game.GameManager.Presenter;

namespace Viking.Scripts.Game.GameManager.View
{
    public class GameManagerView : MonoBehaviour
    {
        [Header("Bar")] 
        [SerializeField] private Slider sliderLifeCharacter;
        [SerializeField] private int maxLives;

        [SerializeField] private TextMeshProUGUI monstersKilledText;

        private GameManagerPresenter _presenter;

        private void Start()
        {
            _presenter = new GameManagerPresenter(this);
            _presenter.Initialize(maxLives, maxLives);

            CheckSerializeFieldToNull();
        }

        public void OnMonsterKilled()
        {
            Debug.Log("GameManagerView OnMonsterKilled");
            _presenter.OnMonsterKilled();
        }

        public void UpdatesSliderLife(int currentLives)
        {
            if (CheckSerializeFieldToNull())
            {
                sliderLifeCharacter.value = currentLives;
            }
        }

        public void UpdateMonstersKilledText(int monstersKilled)
        {
            if (CheckSerializeFieldToNull())
            {
                monstersKilledText.text = "Monsters Killed: " + monstersKilled;
            }
        }


        private bool CheckSerializeFieldToNull()
        {
            if (monstersKilledText == null)
            {
                Debug.LogError(
                    "GameManagerView needs monstersKilledText to present please make sure one is set in The Inspector");
                return false;
            }

            if (sliderLifeCharacter == null)
            {
                Debug.LogError(
                    "GameManagerView  needs a sliderLifeCharacter to Update please make sure one is set in The Inspector");
                return false;
            }


            return true;
        }
    }
}