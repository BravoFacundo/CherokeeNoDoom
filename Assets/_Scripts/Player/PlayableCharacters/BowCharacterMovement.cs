using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCharacterMovement : PlayerMovement
{
    [Header("Dash Skill")]
    [SerializeField] float dashForce = 15f;
    [SerializeField] float dashCD = 3f;
    [SerializeField] private float dashFOVChange = 120f;
    [SerializeField] private float dashFOVReturnTime = 0.5f;
    private bool dashIsOnCD;

    protected override void Update()
    {
        base.Update();
    }    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canMove)
        {
            PlayerDash();
        }
    }
    void PlayerDash()
    {
        if (readyToDash && !dashIsOnCD)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * dashForce / 4, ForceMode.VelocityChange);
            rb.AddForce(moveDirection * dashForce, ForceMode.VelocityChange);

            StartCoroutine(FastFOVChange(dashFOVChange, dashFOVReturnTime));
            StartCoroutine(nameof(DashCooldown), dashCD);

            readyToDash = false;
        }        
    }
    private IEnumerator DashCooldown(float cooldown)
    {
        playerController.playerHUD.DashSkill(false);
        dashIsOnCD = true;

        yield return new WaitForSeconds(cooldown);

        playerController.playerHUD.DashSkill(true);
        dashIsOnCD = false;
        readyToDash = false;
    }

}
