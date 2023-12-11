using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiCharacterMovement : PlayerMovement
{
    [Header("Dash Skill")]
    [SerializeField] float dashLenght = 12f;
    [SerializeField] private float dashFOVChange = 120f;
    [SerializeField] private float dashFOVReturnTime = 0.5f;

    protected override void Update()
    {
        base.Update();

        DashInput();
    }
    void DashInput()
    {
        if (Input.GetKeyDown(dashKey)) readyToDash = true;
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
            transform.position += cam.transform.forward * dashLenght;
            StartCoroutine(FastFOVChange(dashFOVChange, dashFOVReturnTime));
            readyToDash = false;
        }
    }

}
