using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private float health;
    private bool enemyDied = false;
    [SerializeField] private float speed;
    [SerializeField] private float sightRange;
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInAttackRange;
    private bool alreadyAttack = false;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

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
    private void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
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

        target.GetComponent<PlayerController>().PlayerDie(attackAnimPivot);
        agent.obstacleAvoidanceType = 0;
        
        billboardSpriteRenderer.maxRotationX = 0;

        Invoke(nameof(attackAnim), 0.5f);
    }
    private void attackAnim()
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
}
