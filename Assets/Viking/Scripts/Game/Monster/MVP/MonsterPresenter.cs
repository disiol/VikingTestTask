
using UnityEngine;
using Viking.Scripts.Game.Monster.MVP;

namespace Viking.Scripts.Game.MonsterManager.MVP
{
    public class MonsterPresenter
    {
        private readonly MonsterModel _model;
        private readonly MonsterView _view;

        public MonsterPresenter(MonsterModel model, MonsterView view)
        {
            this._model = model;
            this._view = view;

            // Subscribe to the monster's death event
            model.MonsterDeathEvent += OnMonsterDeath;
        }

        public void MonsterHasDamage()
        {
            _model.MonsterHasDamage();
            UpdateUI();
        }

        public void OnMonsterDeath()
        {
            _model.IncreaseLives();
            //TODO Generate momstr near pler
        }

       
        
        private void UpdateUI()
        {
            _view.UpdateLivesIndicator(_model.Lives);
        }
    }

}