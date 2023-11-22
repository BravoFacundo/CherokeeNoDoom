using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCharacterShoot : PlayerShoot
{
    [Header("Bow Configuration")]
    [SerializeField] float shootForce;
    private float currentShootForce;
    [SerializeField] float shootDamage = 100f;
    [SerializeField] float shootChargeSpeed;
    [SerializeField] float shootMaxCharge;
    [SerializeField] float shootReloadTime;
    [SerializeField] float chargeTime;

    [Header("Bow Feedback")]
    [SerializeField] float moveSpeed = 10f;
    private float moveDuration = 2.0f;
    private RectTransform rectTransform;
    private Vector3 initialPos;
    private bool bowIsMoving = false;
    private float moveTimer = 0.0f;

    protected override void Start()
    {
        base.Start();

        rectTransform = weaponObject.GetComponent<RectTransform>();
        initialPos = rectTransform.position;
        moveDuration = shootMaxCharge;
    }

    protected override void Update()
    {
        base.Update();

        NewShootInput();
        //ShootInput();
        
        Animation();
    }

    private void NewShootInput()
    {

    }

    private void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            chargeTime = 0;
            moveTimer = 0.0f;
            bowIsMoving = true;
        }
        if (Input.GetMouseButton(0))
        {
            isCharging = true;
            if (isCharging && !isReloading && chargeTime < shootMaxCharge)
            {
                chargeTime += Time.deltaTime * shootChargeSpeed;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            bowIsMoving = false;
            rectTransform.position = initialPos;

            if (chargeTime <= shootMaxCharge * 0.5) //No Shoot
            {
                chargeTime = 0;
            }
            else
            if (chargeTime >= shootMaxCharge * 0.5 && chargeTime <= shootMaxCharge * 0.75) //Weak Shoot
            {              
                StartCoroutine(ShootProjectile(shootForce * 0.5f, shootReloadTime, shootDamage/2));
                chargeTime = 0;
            }
            else
            if (chargeTime >= shootMaxCharge * 0.75 && chargeTime <= shootMaxCharge + 1) //Hard Shoot
            {
                StartCoroutine(ShootProjectile(shootForce, shootReloadTime, shootDamage));
                chargeTime = 0;
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
