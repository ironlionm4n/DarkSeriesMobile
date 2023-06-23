using UnityEngine;

namespace EnemyAI
{
    [CreateAssetMenu(menuName = "EnemyTraits", fileName = "NewEnemyTrait")]
    public class EnemyTrait : ScriptableObject
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float idleTime = 3f;
        [SerializeField] private float wanderTime = 5f;
        [SerializeField] private float runSpeed = 5f;
        [SerializeField] private float chaseDistance = 10f;
        [SerializeField] private float attackDistance = 2f;

        public EnemyType EnemyType => enemyType;
        public float IdleTime => idleTime;
        public float WanderTime => wanderTime;
        public float ChaseDistance => chaseDistance;
        public float AttackDistance => attackDistance;
        public float RunSpeed => runSpeed;
    }
}