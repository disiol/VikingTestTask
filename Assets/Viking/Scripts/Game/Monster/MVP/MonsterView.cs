using System;
using TMPro;
using UnityEngine;
using Viking.Scripts.Game.MonsterManager.MVP;

namespace Viking.Scripts.Game.MonsterManager
{
    public class MonsterView : MonoBehaviour
    {
        [SerializeField] private GameObject prefabSphereOfLife;
        [SerializeField] private TextMeshPro livesIndicator;

        private MonsterPresenter _presenter;
        private MonsterModel _monsterModel;
        private MonsterView _monsterView;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            this._monsterModel = new MonsterModel();
            this._monsterView = gameObject.GetComponent<MonsterView>();
            this._presenter = new MonsterPresenter(_monsterModel, _monsterView);
        }

        public void OnMonsterDeath()
        {
            _presenter.OnMonsterDeath();
        }

        public void SpawnSphereOfLife(Vector3 position)
        {
            Instantiate(prefabSphereOfLife, position, Quaternion.identity);
        }

        public void UpdateLives(int modelLives)
        {
            livesIndicator.text = modelLives.ToString();
        }
    }
}