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

    [Header("Bow Feedback")]
    [SerializeField] float moveSpeed;
    private float moveDuration = 2.0f;
    private RectTransform rectTransform;
    private Vector3 initialPos;
    private bool bowIsMoving = false;
    private float moveTimer = 0.0f;

    protected override void Start()
    {
        base.Start();
        
        shootChargeSpeed = 1 / shootChargeTime;

        rectTransform = weaponObject.GetComponent<RectTransform>();
        initialPos = rectTransform.position;        
        moveDuration = shootChargeTime;
    }

    protected override void Update()
    {
        base.Update();

        ShootInput();
        
        Animation();
    }

    private void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            chargeTime = 0;
            animator.SetBool("IsLoading", true);

            //moveTimer = 0.0f;
            //bowIsMoving = true;
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
            //bowIsMoving = false;
            //rectTransform.position = initialPos;

            if (!isReloading)
            {
                var newShootForce = Mathf.Lerp(shootForceRange.x, shootForceRange.y, chargeTime);
                var newShootDamage = Mathf.Lerp(shootDamageRange.x, shootDamageRange.y, chargeTime);
                StartCoroutine(ShootProjectile(newShootForce, shootReloadTime, newShootDamage));
                //print("Shoot Force: " + newShootForce + " | Shoot Damage: " + newShootDamage);

                chargeTime = 0;
                animator.SetBool("IsLoading", false);
            }          
        }

    }

    private void Animation()
    {
        if (bowIsMoving)
        {
            if (moveTimer < moveDuration)
            {
                float distance = rectTransform.sizeDelta.y * 0.5f;
                float step = moveSpeed * Time.deltaTime;
                Vector3 targetPosition = initialPos - new Vector3(0, distance, 0);
                rectTransform.position = Vector3.MoveTowards(rectTransform.position, targetPosition, step);
                moveTimer += Time.deltaTime;
            }
            else
            {
                bowIsMoving = false;
            }
        }
    }

}
