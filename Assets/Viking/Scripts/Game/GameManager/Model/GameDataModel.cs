using System;

namespace Viking.Scripts.Game.Model
{

    public class GameDataModel
    {
        private int _maxLives;
        private int _currentLives;
        private int _monstersKilled;

        public int MaxLives
        {
            get => _maxLives;
            set
            {
                _maxLives = value;
                OnMaxLivesChanged?.Invoke(_maxLives);
            }
        }

        public int CurrentLives
        {
            get => _currentLives;
            set
            {
                _currentLives = value;
                OnCurrentLivesChanged?.Invoke(_currentLives);
            }
        }

        public int MonstersKilled
        {
            get => _monstersKilled;
            set
            {
                _monstersKilled = value;
                OnMonstersKilledChanged?.Invoke(_monstersKilled);
            }
        }

        public event Action<int> OnMaxLivesChanged;
        public event Action<int> OnCurrentLivesChanged;
        public event Action<int> OnMonstersKilledChanged;
    }

}