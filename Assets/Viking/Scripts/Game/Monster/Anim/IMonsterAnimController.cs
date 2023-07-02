
namespace Viking.Scripts.Game.Monster.Anim
{
    public interface IMonsterAnimController
    {
        void Run();
        void Stop();
        void Attack(bool value);
        void Damage(bool value);
        void Die();

    }
}