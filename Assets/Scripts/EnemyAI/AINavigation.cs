using System;
using System.Collections;
using EnemyAI;
using UnityEngine;
using Random = UnityEngine.Random;

public class AINavigation : MonoBehaviour
{
    [SerializeField] private EnemyTrait enemyTrait;  // your scriptable object
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector3 _randomDirection;
    private Transform _playerTransform;
    private static readonly int IsRunningRight = Animator.StringToHash("isRunningRight");
    private static readonly int IsRunningLeft = Animator.StringToHash("isRunningLeft");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
        switch (enemyTrait.EnemyType)
        {
            case EnemyType.Warrior:
            {
                StartCoroutine(WarriorStateMachine());
                break;
            }
            default: Debug.Log("DEFAULT CASE HIT AI NAVIGATION");
                break;
        }
        
    }

    private void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, _playerTransform.position));
    }

    private IEnumerator WarriorStateMachine()
    {
        while (true)
        {
            yield return Idle();
            yield return Wander();
        }
    }

    private IEnumerator Idle()
    {
        _animator.SetBool(IsRunningRight, false);
        _animator.SetBool(IsRunningLeft, false);
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(enemyTrait.IdleTime);
        _randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    private IEnumerator Wander()
    {
        float startTime = Time.time;

        while (Time.time - startTime < enemyTrait.WanderTime)
        {
            if (_rb.velocity.x < 0)
            {
                _animator.SetBool(IsRunningLeft, true);
                _animator.SetBool(IsRunningRight, false);
            }
            else
            {
                _animator.SetBool(IsRunningLeft, false);
                _animator.SetBool(IsRunningRight, true);
            }
            
            _rb.velocity = _randomDirection * enemyTrait.RunSpeed;

            // Check if the player is within the chase distance.
            if (Vector3.Distance(transform.position, _playerTransform.position) < enemyTrait.ChaseDistance)
            {
                yield return Chase();
            }

            yield return null;
        }
    }

    private IEnumerator Chase()
    {
        float chaseCheckInterval = 1f;  // Recheck every second.

        while (true)
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            _rb.velocity = directionToPlayer * enemyTrait.RunSpeed;

            // If the player is in the attack distance, switch to Attack state.
            if (Vector3.Distance(transform.position, _playerTransform.position) <= enemyTrait.AttackDistance)
            {
                yield return Attack();
                break;
            }

            // If the player is out of chase distance, break the chase.
            if (Vector3.Distance(transform.position, _playerTransform.position) > enemyTrait.ChaseDistance)
            {
                break;
            }

            yield return new WaitForSeconds(chaseCheckInterval);
        }
    }

    private IEnumerator Attack()
    {
        _rb.velocity = Vector2.zero;
        _animator.SetBool(IsAttacking, true);

        yield return new WaitUntil(() => !_animator.GetBool(IsAttacking));
        
        // After attacking, if player is still within chase distance, chase again.
        if (Vector3.Distance(transform.position, _playerTransform.position) < enemyTrait.ChaseDistance)
        {
            yield return Chase();
        }
    }

    public void EndAttack()
    {
        _animator.SetBool(IsAttacking, false);
    }

}
