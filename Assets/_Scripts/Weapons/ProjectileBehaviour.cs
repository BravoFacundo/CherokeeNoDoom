using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [Header("Values")]
    public float initialSpeed = 110f;
    public float gravity = -9.81f;
    public float checkDistance = 1f;
    public float shootDamage;
    
    [Header("Data")]
    [SerializeField] List<Collider> ignoredColliders = new();
    [SerializeField] RaycastHit[] hits;

    [Header("References")]
    [SerializeField] private EnemyHitData hitData; 

    [Header("Local References")]
    [SerializeField] GameObject colliderObj;
    [SerializeField] GameObject modelObj;
    private Rigidbody rb;

    private void Awake()
    {
        colliderObj.SetActive(false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = rb.velocity;
        float timeStep = Time.deltaTime;

        hits = Physics.RaycastAll(currentPosition, currentVelocity.normalized, checkDistance);
        Debug.DrawRay(currentPosition, currentVelocity.normalized * checkDistance, Color.red);

        foreach (RaycastHit hit in hits)
        {
            hitData = hit.transform.GetComponent<EnemyHitData>();
            if (hitData != null && hitData.spriteRenderer != null)
            {
                Vector2 textureCoord = hit.textureCoord;

                Texture2D animTexture = hitData.spriteRenderer.sprite.texture;
                Texture2D dataTexture = hitData.dataTexture;

                int uvX = Mathf.FloorToInt(textureCoord.x * animTexture.width);
                int uvY = Mathf.FloorToInt(textureCoord.y * animTexture.height);

                Color dataPixelColor = dataTexture.GetPixel(uvX, uvY);
                Color hitPixelColor = animTexture.GetPixel(uvX, uvY);

                if (dataPixelColor.a > GameConstants.ALPHA_THRESHOLD)
                {
                    foreach (Collider ignoredCollider in ignoredColliders)
                    {
                        Physics.IgnoreCollision(colliderObj.GetComponent<Collider>(), ignoredCollider, false);
                    }
                    ignoredColliders.Clear();

                    transform.position = hit.point;
                    rb.isKinematic = true;
                    Destroy(gameObject);

                    hitData.enemyScript.EnemyDamage(shootDamage * 2);
                }
                else
                if (hitPixelColor.a > GameConstants.ALPHA_THRESHOLD)
                {
                    foreach (Collider ignoredCollider in ignoredColliders)
                    {
                        Physics.IgnoreCollision(colliderObj.GetComponent<Collider>(), ignoredCollider, false);
                    }
                    ignoredColliders.Clear();
                    
                    transform.position = hit.point;
                    rb.isKinematic = true;
                    Destroy(gameObject);

                    hitData.enemyScript.EnemyDamage(shootDamage);

                    break; // Salir del bucle al encontrar un hit válido
                }
            }
        }

        currentPosition += currentVelocity * timeStep;
        currentVelocity += Vector3.up * gravity * timeStep;
    }
}
