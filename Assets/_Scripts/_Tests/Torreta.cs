using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    [Header("Configuration")]
    public float shootForce;
    [SerializeField] private float shootDelay;
    private float shootForceBackup;
    private ForceMode forceMode;

    [Header("References")]
    [SerializeField] private GameObject bowObject;

    [Header("Prefabs")]
    [SerializeField] private Rigidbody arrowPrefab;

    private void Start()
    {
        shootForceBackup = shootForce;

        StartCoroutine(nameof(Shooting));
    }

    IEnumerator Shooting()
    {
        Rigidbody arrow = arrowPrefab;
        Vector3 spawnPosition = Vector3.zero;

        spawnPosition = bowObject.transform.position;

        Rigidbody newArrow = Instantiate(arrow, spawnPosition, Quaternion.identity) as Rigidbody;
        newArrow.AddForce(bowObject.transform.forward * shootForce, ForceMode.VelocityChange);
        //newArrow.transform.Rotate(0, 0, Random.Range(0, 180), Space.Self);

        yield return new WaitForSeconds(shootDelay);
        shootForce = shootForceBackup;
        StartCoroutine(nameof(Shooting));
    }

}
