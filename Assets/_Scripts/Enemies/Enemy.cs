using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float speedGain;
    [SerializeField] private float speedGainInterval;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;

    [Header("States")]
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool playerInAttackRange;
    private bool enemyDied = false;
    private bool alreadyAttack = false;

    [Header("Configuration")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    [Header("Local References")]
    [SerializeField] private CustomBillboardRenderer billboardSpriteRenderer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Animator enemyAttackAnimator;
    [SerializeField] Transform attackAnimPivot;

    [Header("References")]
    [SerializeField] Transform target;

    private float elapsedTime = 0f;

    private void Awake()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();       
    }

    private void Start()
    {
        agent.speed = speed;
    }

    private void Update()
    {
        StateMachine();
        ControlSpeed();
    }

    void StateMachine()
    {
        if (!enemyDied && alreadyAttack == false)
        {
            if (playerInSightRange)
            {
                if (!playerInAttackRange) ChasePlayer();
                else AttackPlayer();
            }
            else if (!playerInAttackRange) WaitForPlayer();
        }
    }

    void ControlSpeed()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= speedGainInterval)
        {
            speed += speedGain;
            agent.speed = speed;
            elapsedTime = 0f;
        }
    }

    private void WaitForPlayer()
    {
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("Run", false);
        agent.SetDestination(transform.position);
    }
    private void ChasePlayer()
    {
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("Run", true);
        agent.SetDestination(target.position);
    }
    private void AttackPlayer()
    {
        alreadyAttack = true;
        agent.SetDestination(transform.position);        
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("Run", false);

        billboardSpriteRenderer.maxRotationX = -5f;
        agent.obstacleAvoidanceType = 0;

        var playerController = target.GetComponent<PlayerController>();
        if (playerController.isAlive) Invoke(nameof(AttackAnim), 0);
        playerController.PlayerDie(attackAnimPivot);
        
    }
    private void AttackAnim()
    {
        enemyAnimator.SetTrigger("Hide");
        enemyAttackAnimator.SetTrigger("Attack");
    }

    public void EnemyDie()
    {
        enemyDied = true;
        enemyAnimator.SetBool("Death", true);
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("Run", false);
        agent.SetDestination(transform.position);
        Destroy(gameObject, 5f);
    }
    public void EnemyDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !enemyDied) EnemyDie();
        else StartCoroutine(nameof(EnemyHurt));
    }
    private IEnumerator EnemyHurt()
    {
        agent.speed = 0f;
        yield return new WaitForSeconds(0.5f);
        agent.speed = speed;
    }

    private void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    }
}
