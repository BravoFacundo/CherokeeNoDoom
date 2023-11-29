using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BowCharacterShoot : PlayerShoot
{
    [Header("Bow Configuration")]
    [SerializeField] float chargeTime;
    [SerializeField] float shootChargeTime; float shootChargeSpeed;
    [SerializeField] Vector2 shootForceRange; //Original is 25 - 110 m/s
    [SerializeField] Vector2 shootDamageRange;
    [SerializeField] float shootReloadTime;
    [SerializeField] float aimSpeedReduction;

    [Header("Bow Animation")]
    [SerializeField] Animator animator;

    protected override void Start()
    {
        base.Start();

        SetanimationSpeed();
    }

    private void SetanimationSpeed()
    {
        shootChargeSpeed = 1 / shootChargeTime;
        animator.SetFloat("Speed_Load", shootChargeSpeed);

        var shootReloadSpeed = 2 / shootReloadTime;
        animator.SetFloat("Speed_Release", shootReloadSpeed);
        animator.SetFloat("Speed_Reload", shootReloadSpeed);
    }

    protected override void Update()
    {
        base.Update();

        ShootInput();
    }

    private void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            chargeTime = 0;
            animator.SetBool("IsLoading", true);
        }
        if (Input.GetMouseButton(0))
        {
            isCharging = true;
            if (isCharging && !isReloading)
            {
                if (chargeTime <= 1) chargeTime += Time.deltaTime * shootChargeSpeed;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {            
            if (!isReloading)
            {                
                var newShootForce = Mathf.Lerp(shootForceRange.x, shootForceRange.y, chargeTime);
                var newShootDamage = Mathf.Lerp(shootDamageRange.x, shootDamageRange.y, chargeTime);
                StartCoroutine(ShootProjectile(newShootForce, shootReloadTime, newShootDamage)); //print("Shoot Force: " + newShootForce + " | Shoot Damage: " + newShootDamage);

                chargeTime = 0;
                animator.SetBool("IsLoading", false);
                animator.SetTrigger("Release");
            }          
        }
    }

}
