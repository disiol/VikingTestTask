using UnityEngine;
using Viking.Scripts.Game.Player.Anim;

namespace Viking.Scripts.Game.Monster.Anim
{
    public class MonsterAnimController : MonoBehaviour, IMonsterAnimController
    {
        private Animator _animator;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Run1 = Animator.StringToHash("Run");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int Damage1 = Animator.StringToHash("Damage");
        private static readonly int Die1 = Animator.StringToHash("Die");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Run()
        {
            _animator.SetBool(Run1, true);
            _animator.SetBool(Idle, false);
        }

        public void Stop()
        {
            _animator.SetBool(Idle, true);
            _animator.SetBool(Run1, false);
        }

        public void Attack(bool value)
        {
            _animator.SetBool(Attack1, value);
        }

        public void Damage(bool value)
        {
            _animator.SetBool(Damage1, value);

        }

        public void Die()
        {
            _animator.SetBool(Die1, true);
        }
    }
}