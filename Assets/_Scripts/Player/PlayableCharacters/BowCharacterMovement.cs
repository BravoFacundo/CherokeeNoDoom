using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCharacterMovement : PlayerMovement
{
    [Header("Character Skills Inputs")]
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;

    [Header("Dash Skill")]
    [SerializeField] float dashForce = 15f;
    [SerializeField] private float dashFOVChange = 120f;
    [SerializeField] private float dashFOVReturnTime = 0.5f;

    protected override void Update()
    {
        base.Update();

        DashInput();
    }
    void DashInput()
    {
        if (Input.GetKeyDown(dashKey)) PlayerDash();
    }
    void PlayerDash()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * dashForce/4, ForceMode.VelocityChange);
        rb.AddForce(moveDirection * dashForce, ForceMode.VelocityChange);
        StartCoroutine(FastFOVChange(dashFOVChange, dashFOVReturnTime));
    }

}
