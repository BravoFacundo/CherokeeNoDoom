using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEnemy : MonoBehaviour
{
    public Transform player;
    public Rigidbody rigibody;
    public bool canMove = true;
    //public LayerMask playerMask;

    public int health;
    public float speed;
    public bool playerInAttackRange = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        rigibody = gameObject.GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        //if (!playerInAttackRange) ChasePlayer();
        //if (playerInAttackRange) AttackPlayer();
        transform.LookAt(player);
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.name == "DeathArea")
        {
            playerInAttackRange = true;
        }
    }

    private void ChasePlayer()
    {
        if (canMove)
        {
            transform.LookAt(player); //No deberian moverse en Y.
            rigibody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Acceleration);
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y ,player.position.z), speed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        canMove = false;        
        transform.LookAt(player);

        //Attack code here
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) EnemyDeath();
    }
    private IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
