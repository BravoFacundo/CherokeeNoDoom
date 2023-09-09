using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiCharacterMovement : PlayerMovement
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
        transform.position += cam.transform.forward * 12f;
    }

}
