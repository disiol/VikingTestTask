using UnityEngine;

namespace Viking.Scripts.Game.Player.PlayerHealth
{
    public class PlayerHealthModel
    {
        private int maxHealth;
        private int currentHealth;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        public PlayerHealthModel(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

}