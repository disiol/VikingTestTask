
using UnityEngine;

namespace Viking.Scripts.Game.MonsterManager.MVP
{
    public class MonsterPresenter:MonoBehaviour
    {
        private MonsterModel _model;
        private MonsterView _view;

        public MonsterPresenter(MonsterModel model, MonsterView view)
        {
            this._model = model;
            this._view = view;

            // Subscribe to the monster's death event
            model.MonsterDeathEvent += OnMonsterDeath;
        }

        public void OnMonsterHasDemege()
        {
            
        }

        public void OnMonsterDeath()
        {
            _model.IncreaseLives();
            // Perform other logic, such as respawning the monster

            // Spaw the sphere of life
            SpawnSphereOfLife();
            

            // Update the _view or notify other components about the changes
        }

        private void SpawnSphereOfLife()
        {
            _view.SpawnSphereOfLife(transform.position);
            // TODO Code to spawn the sphere of life
        }
        
        private void UpdateUI()
        {
            _view.UpdateLives(_model.Lives);
        }
    }

}