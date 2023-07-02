using System;
using UnityEngine;
using Viking.Scripts.Game.GameManager.Model;
using Viking.Scripts.Game.GameManager.View;

namespace Viking.Scripts.Game.GameManager.Presenter
{
    public class GameManagerPresenter
    {
        private readonly GameManagerView _view;
        private readonly GameDataModel _model;

        public GameManagerPresenter(GameManagerView view)
        {
            this._view = view;
            _model = new GameDataModel();
        }

        public void Initialize(int modelMaxLives, int modelCurrentLives)
        {
            _model.MaxLives = modelMaxLives;
            _model.CurrentLives = modelCurrentLives;
            _model.MonstersKilled = 0;

            _model.OnCurrentLivesChanged += _view.UpdatesSliderLife;
            _model.OnMonstersKilledChanged += _view.UpdateMonstersKilledText;

            UpdateUI();
        }
        

        public void OnMonsterKilled()
        {
            Debug.Log("GameManagerPresenter OnMonsterKilled");

            _model.MonstersKilled++;
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            _view.UpdatesSliderLife(_model.CurrentLives);
            _view.UpdateMonstersKilledText(_model.MonstersKilled);
        }

   
    }
}