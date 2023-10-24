using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Configuration")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    NavMeshAgent agent;

    [Header("States")]
    [SerializeField] private float sightRange;
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInAttackRange;
    private bool alreadyAttack = false;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("Animator")]
    private Animator enemyAnimator;
    private bool enemyDied = false;

    [Header("References")]
    [SerializeField] Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = gameObject.GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!enemyDied && alreadyAttack == false)
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
        agent.SetDestination(target.position);
    }
    private void AttackPlayer()
    {
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("Run", false);
        agent.SetDestination(transform.position);
        alreadyAttack = true;

        //Hardcoded
        GameObject.Find("Death").GetComponent<Image>().enabled = true;
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
}
