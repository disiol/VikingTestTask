using UnityEngine;

namespace Viking.Scripts.Game.Player.PlayerHealth
{
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            healthSlider.value = currentHealth;
            healthSlider.maxValue = maxHealth;
        }
    }
}