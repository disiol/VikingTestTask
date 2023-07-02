using UnityEngine;

namespace Viking.Scripts.Game.Player.PlayerHealth
{
    public class PlayerHealthPresenter
    {
        private PlayerHealthModel model;
        private PlayerHealthView view;

        public PlayerHealthPresenter(PlayerHealthModel model, PlayerHealthView view)
        {
            this.model = model;
            this.view = view;
        }

        public void TakeDamage(int amount)
        {
            model.TakeDamage(amount);
            UpdateView();
        }

        public void Heal(int amount)
        {
            model.Heal(amount);
            UpdateView();
        }

        private void UpdateView()
        {
            view.UpdateHealth(model.CurrentHealth, model.MaxHealth);
        }
    }

}