using UnityEngine;
using Viking.Scripts.Game.Monster.Anim;
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

        private void Start()
        {
            _monsterAnimController = gameObject.GetComponent<MonsterAnimController>();
            player = GameObject.FindGameObjectWithTag("Player");
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
                    MoveToPlayer();
                }
            }
        }

        private void MoveToPlayer()
        {
            agent.SetDestination(player.transform.position);
        }

        private void AttackPlayer()
        {
            // Perform attack logic here
            // For example, you could reduce the player's health or trigger other effects
            Debug.Log("Monster attacked player!");

            player.GetComponent<PlayerHealthPresenter>().TakeDamage(damageAmount);

            canAttack = false;
            Invoke("ResetAttackCooldown", attackCooldown);
        }

        private void ResetAttackCooldown()
        {
            canAttack = true;
        }
    }
}