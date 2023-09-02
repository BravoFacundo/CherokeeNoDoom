using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{   

    [Header("Configuration")]
    public float shootForce;
    private float shootForceBackup;
    private ForceMode forceMode;

    [SerializeField] private float shootChargeSpeed;
    [SerializeField] private float shootMaxCharge;
    [SerializeField] private float shootReloadTime;

    [Header("Debug")]
    public bool isReloading;
    private bool isCharging;
    [SerializeField] private float chargeTime;

    [Header("References")]
    [SerializeField] private GameObject bowObject;
    private Camera cam;

    [Header("Prefabs")]
    [SerializeField] private Rigidbody arrowPrefab;
    [SerializeField] private Rigidbody arrowForkPrefab;

    void Start()
    {
        cam = Camera.main;

        shootForceBackup = shootForce;
        isReloading = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2));
        if (Input.GetMouseButtonDown(1));

        if (Input.GetKeyDown(KeyCode.E))
        //if (Input.GetMouseButtonDown(0))
        {            
            //print("Start Charging Shoot");
            chargeTime = 0;

        }
        if (Input.GetKey(KeyCode.E))
        //if (Input.GetMouseButton(0))
        {            
            isCharging = true;
            //bowAnimator.SetTrigger("Arrow_Charge");
            if (isCharging && !isReloading && chargeTime < shootMaxCharge)
            {
                chargeTime += Time.deltaTime * shootChargeSpeed;
                //bowAnimator.SetBool("Arrow_Charge", true);
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        //if (Input.GetMouseButtonUp(0))
        {            
            if (chargeTime <= shootMaxCharge * 0.5)
            {
                //print("No Shoot");
                chargeTime = 0;
            }
            else
            if (chargeTime >= shootMaxCharge * 0.5 && chargeTime <= shootMaxCharge * 0.75)
            {
                //print("Weak Shoot");
                StartCoroutine(nameof(ShootArrow), 1);
            }
            else
            if (chargeTime >= shootMaxCharge * 0.75 && chargeTime <= shootMaxCharge + 1)
            {
                //print("Hard Shoot");
                StartCoroutine(nameof(ShootArrow), 2);
            }
        }
    }

    IEnumerator ShootArrow(int shootDamage)
    {
        isCharging = false;
        chargeTime = 0;
        isReloading = true;

        //Shoot Direction
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(ray.direction);

        Rigidbody arrow = arrowPrefab;
        Vector3 spawnPosition = Vector3.zero;
        
        //spawnPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane + .5f));
        spawnPosition = cam.transform.position;

        Rigidbody newArrow = Instantiate(arrow, spawnPosition, rotation) as Rigidbody;
        newArrow.AddForce(ray.direction * shootForce, ForceMode.VelocityChange);
        //newArrow.transform.Rotate(0, 0, Random.Range(0, 180), Space.Self);

        yield return new WaitForSeconds(shootReloadTime);
        shootForce = shootForceBackup;
        isReloading = false;
    }

}



    
