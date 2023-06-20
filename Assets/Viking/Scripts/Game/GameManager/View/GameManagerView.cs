using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Viking.Scripts.Game.GameManager.Presenter;

namespace Viking.Scripts.Game.GameManager.View
{
    public class GameManagerView : MonoBehaviour
    {
        [Header("Bar")]
        [SerializeField] private Slider progressBar;
        [SerializeField] private Text monstersKilledText;

        private GameManagerPresenter _presenter;
        [FormerlySerializedAs("MaxLives")] [SerializeField] private int maxLives;

        private void Start()
        {
            _presenter = new GameManagerPresenter(this);
            _presenter.Initialize(maxLives, maxLives);
        }

        public void OnMonsterKilled()
        {
            _presenter.OnMonsterKilled();
        }

        public void UpdateProgressBar(int currentLives)
        {
            progressBar.value = currentLives;
        }

        public void UpdateMonstersKilledText(int monstersKilled)
        {
            monstersKilledText.text = "Monsters Killed: " + monstersKilled;
        }
    }
}