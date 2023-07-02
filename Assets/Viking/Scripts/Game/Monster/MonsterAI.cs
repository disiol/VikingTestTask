using UnityEngine;
using Viking.Scripts.Game.Monster.Anim;
using Viking.Scripts.Game.Player.Anim;
using Viking.Scripts.Game.Player.PlayerHealth;

namespace Viking.Scripts.Game.Monster
{
    using UnityEngine;
    using UnityEngine.AI;

    public class MonsterAI : MonoBehaviour
    {
        [SerializeField] private float attackRange = 2f;

        [SerializeField] private int damageAmount = 10;

        [SerializeField] private float attackCooldown = 1f;

        private MonsterAnimController _monsterAnimController;

        private GameObject player;
        private NavMeshAgent agent;
        private bool canAttack = true;
        private PlayerAnimController _playerAnimController;

        private void Start()
        {
            _monsterAnimController = gameObject.GetComponent<MonsterAnimController>();

            player = GameObject.FindGameObjectWithTag("Player");
            _playerAnimController = player.GetComponent<PlayerAnimController>();

            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (player != null)
            {
                if (canAttack && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    
                    AttackPlayer();
                }
                else
                {
                    _playerAnimController.Damage(false);
                    _monsterAnimController.Attack(false);

                    MoveToPlayer();
                }
            }
        }

        private void MoveToPlayer()
        {
            
            agent.SetDestination(player.transform.position);
            _monsterAnimController.Run();
        }

        private void AttackPlayer()
        {
            _monsterAnimController.Stop();
            // Perform attack logic here
            // For example, you could reduce the player's health or trigger other effects
            Debug.Log("Monster attacked player!");
            _monsterAnimController.Attack(true);

            player.GetComponent<PlayerHealthPresenter>().TakeDamage(damageAmount);
            _playerAnimController.Damage(true);
            canAttack = false;
            Invoke("ResetAttackCooldown", attackCooldown);
        }

        private void ResetAttackCooldown()
        {
            canAttack = true;
        }
    }
}