using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenCharacterMovement : PlayerMovement
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
        if (Input.GetKeyDown(dashKey))
        {
            col.isTrigger = true;
            PlayerDash();
            Invoke(nameof(PlayerDashIFrameReset), .025f);
        }
    }
    void PlayerDash() => rb.AddForce(2 * jumpForce * cam.transform.forward, ForceMode.VelocityChange);
    void PlayerDashIFrameReset() => col.isTrigger = false;

}
