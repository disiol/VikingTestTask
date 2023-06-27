using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Viking.Scripts.Game.Monster.MVP;
using Viking.Scripts.Game.MonsterManager.MVP;

namespace Viking.Scripts.Game.MonsterManager
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
            livesIndicator.text = modelLives.ToString();
        }
    }
}