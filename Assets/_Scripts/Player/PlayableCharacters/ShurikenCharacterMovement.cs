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
    private bool readyToDash;

    protected override void Update()
    {
        base.Update();

        DashInput();
    }
    void DashInput()
    {
        if (Input.GetKeyDown(dashKey))
        {
            readyToDash = true;
            col.isTrigger = true;
            Invoke(nameof(PlayerDashIFrameReset), .025f);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        PlayerDash();
    }
    void PlayerDash()
    {
        if (readyToDash)
        {
            rb.AddForce(dashForce * cam.transform.forward, ForceMode.VelocityChange);
            StartCoroutine(FastFOVChange(dashFOVChange, dashFOVReturnTime));
            readyToDash = false;
        }
    }
    void PlayerDashIFrameReset() => col.isTrigger = false;

}
