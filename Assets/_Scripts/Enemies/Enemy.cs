using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private Transform player;
    NavMeshAgent agent;
    public LayerMask groundLayer, playerLayer;

    [Header("States")]
    [SerializeField] private float sightRange;
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInAttackRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("Animator")]
    private Animator enemyAnimator;
    private bool enemyDied = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!enemyDied)
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                WaitForPlayer();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            else if (playerInSightRange && playerInAttackRange)
            {
                AttackPlayer();
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) EnemyDie();
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
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        enemyAnimator.SetBool("Idle", true);
        agent.SetDestination(transform.position);
    }
    private void EnemyDie()
    {
        enemyDied = true;
        enemyAnimator.SetBool("Death", true);
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("Run", false);
        agent.SetDestination(transform.position);
    }
}
