using UnityEngine;

namespace Viking.Scripts.Game.Monster.MVP
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
        }

        public void MonsterHasDamage()
        {
            Debug.Log("MonsterPresenter MonsterHasDamage ");
            _model.MonsterHasDamage();
            UpdateUI();
        }

        public void OnMonsterDeath()
        {
            Debug.Log("MonsterPresenter OnMonsterDeath ");

            _model.IncreaseLives();
            //TODO Generate momstr near pler
        }

       
        
        private void UpdateUI()
        {
            _view.UpdateLivesIndicator(_model.Lives);
        }
    }

}