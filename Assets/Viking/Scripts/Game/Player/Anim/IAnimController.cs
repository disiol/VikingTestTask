using Codice.CM.Triggers;

namespace Viking.Scripts.Game.Player.Anim
{
    public interface IAnimController
    {
        void Run();
        void Stop();
        void Attack(bool value);
        void Damage(bool value);
        void Die();

    }
}