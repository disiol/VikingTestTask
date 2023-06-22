using System;
using UnityEngine;

namespace Viking.Scripts.Game.Player.Anim
{
    public class PlayerAnimController : MonoBehaviour, IAnimController
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
            _animator.SetBool(Idle, false);
            _animator.SetBool(Run1, true);
        }

        public void Stop()
        {
            _animator.SetBool(Idle, true);
            _animator.SetBool(Run1, false);
        }

        public void Attack()
        {
            _animator.SetBool(Attack1, true);
        }

        public void Damage()
        {
            _animator.SetBool(Damage1, true);

        }

        public void Die()
        {
            _animator.SetBool(Die1, true);
        }
    }
}