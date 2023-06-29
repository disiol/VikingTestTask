using TMPro;
using UnityEngine;

namespace Viking.Scripts.Game.Monster.MVP
{
    public class MonsterView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro livesIndicator;

        [HideInInspector] public MonsterPresenter Presenter;
        [HideInInspector] public MonsterView monsterView;
        [HideInInspector] public MonsterModel MonsterModel;

        private void Start()
        {
            Initialize();
            CheckSerializeFieldToNull();
        }

        private void Initialize()
        {
            this.MonsterModel = new MonsterModel();
            this.monsterView = gameObject.GetComponent<MonsterView>();
            this.Presenter = new MonsterPresenter(MonsterModel, monsterView);
        }

        public void OnMonsterDeath()
        {
            Presenter.OnMonsterDeath();
        }


        public void UpdateLivesIndicator(int modelLives)
        {
            CheckSerializeFieldToNull();
            livesIndicator.text = modelLives.ToString();
        }
        
        
        private bool CheckSerializeFieldToNull()
        {
            if (livesIndicator == null)
            {
                Debug.Log(
                    "MonsterView needs livesIndicator to present please make sure one is set in The Inspector");
                return false;
            }
            

            return true;
        }

    }
}