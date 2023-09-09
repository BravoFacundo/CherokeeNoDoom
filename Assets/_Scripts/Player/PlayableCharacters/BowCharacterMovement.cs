using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCharacterMovement : PlayerMovement
{
    [Header("Character Skills Inputs")]
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;

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
        rb.AddForce(transform.up * jumpForce/4, ForceMode.VelocityChange);
        rb.AddForce(moveDirection * jumpForce, ForceMode.VelocityChange);
    }

}
