using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenCharacterMovement : PlayerMovement
{
    [Header("Character Skills Inputs")]
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;

    [Header("Dash Skill")]
    [SerializeField] float dashForce = 30f;
    [SerializeField] private float dashFOVChange = 120f;
    [SerializeField] private float dashFOVReturnTime = 0.5f;

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
    void PlayerDash()
    {
        rb.AddForce(dashForce * cam.transform.forward, ForceMode.VelocityChange);
        StartCoroutine(FastFOVChange(dashFOVChange, dashFOVReturnTime));
    }
    void PlayerDashIFrameReset() => col.isTrigger = false;

}
