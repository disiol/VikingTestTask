using System;

namespace Viking.Scripts.Game.MonsterManager
{
    public class MonsterModel
    {
        private int lives;

        public int Lives => lives;

        public event Action MonsterDeathEvent;

        public MonsterModel()
        {
            lives = 1;
        }

        public void IncreaseLives()
        {
            lives++;
        }

        public void OnMonsterDeath()
        {
            MonsterDeathEvent?.Invoke();
        }
    }
}