using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [Header("Configuration")]
    public ForceMode forceMode;

    [Header("States")]
    public bool isCharging = false;
    public bool isWaiting = false;
    public bool isReloading = false;

    [Header("Prefabs")]
    public Rigidbody projectilePrefab;

    [Header("References")]
    public PlayerMovement playerMovement;
    public GameObject weaponObject;
    private Camera cam;

    protected virtual void Start()
    {
        cam = Camera.main;
        
        if (weaponObject == null) weaponObject = GameObject.Find("WeaponHolder").transform.GetChild(0).gameObject;
    }

    protected virtual void Update()
    {

    }

    public IEnumerator ShootProjectile(float _shootForce, float _shootReloadTime, float _shootDamage)
    {
        isCharging = false;
        isReloading = true;

        Vector3 spawnPosition = cam.transform.position;
        Ray ray = new(cam.transform.position, cam.transform.forward);
        Quaternion rotation = Quaternion.LookRotation(ray.direction);

        Rigidbody newProjectile = Instantiate(projectilePrefab, spawnPosition, rotation);
        newProjectile.AddForce(ray.direction * _shootForce, forceMode);
        newProjectile.GetComponent<ProjectileBehaviour>().shootDamage = _shootDamage;

        yield return new WaitForSeconds(_shootReloadTime);
        isReloading = false;
    }
}
