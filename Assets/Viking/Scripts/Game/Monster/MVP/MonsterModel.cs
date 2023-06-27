using System;

namespace Viking.Scripts.Game.Monster.MVP
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

        public void MonsterHasDamage()
        {
            lives--;

            if (lives < 0) ;
            {
                OnMonsterDeath();
            }
        }
    }
}