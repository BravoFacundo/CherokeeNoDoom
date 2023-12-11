using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerLook playerLook;
    [HideInInspector] public PlayerShoot playerShoot;
    [HideInInspector] public PlayerHUD playerHUD;

    [HideInInspector] public bool isAlive = true;

    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerLook = GetComponentInChildren<PlayerLook>();
        playerShoot = GetComponentInChildren<PlayerShoot>();

        playerHUD = GameObject.FindWithTag("HUD").transform.GetComponent<PlayerHUD>();
    }

    public void PlayerDie(Transform target)
    {
        if (isAlive)
        {
            isAlive = false;

            playerMovement.rb.constraints = RigidbodyConstraints.FreezeAll;
            playerMovement.canMove = false;

            playerShoot.canShoot = false;

            playerLook.lookTarget = target;
            playerLook.readyToLookTarget = true;

            playerHUD.DeathScreen(true);
        }
    }
}
