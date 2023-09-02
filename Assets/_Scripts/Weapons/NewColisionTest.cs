using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewColisionTest : MonoBehaviour
{
    private GameObject colliderObj;
    private Transform colliderTransform;

    public float initialSpeed = 225f;

    private float gravity = -9.81f;
    private Rigidbody rb;

    private void Awake()
    {
        colliderObj = transform.GetChild(0).gameObject;
        colliderTransform = colliderObj.transform;
        colliderObj.SetActive(false);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * initialSpeed;
        //Time.timeScale = 0.1f;
    }

    private void Update()
    {
        // Simulación de movimiento parabólico
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = rb.velocity;
        float timeStep = Time.deltaTime;

        // Realizar un raycast en la dirección de movimiento
        RaycastHit hit;
        Vector3 rayDirection = currentVelocity.normalized;
        if (Physics.Raycast(currentPosition, rayDirection, out hit, 7f))
        {
            Transform hitTransform = hit.transform;
            Transform parentTransform = hitTransform.parent;

            if (hitTransform.CompareTag("Enemy") && parentTransform != null)
            {
                SpriteRenderer spriteRenderer = parentTransform.GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    Vector2 textureCoord = hit.textureCoord;

                    Texture2D dataTexture = (Texture2D)hit.collider.GetComponent<MeshRenderer>().material.mainTexture;
                    Texture2D animTexture = (Texture2D)spriteRenderer.sprite.texture;

                    int uvX = Mathf.FloorToInt(textureCoord.x * animTexture.width);
                    int uvY = Mathf.FloorToInt(textureCoord.y * animTexture.height);

                    Color hitColor = animTexture.GetPixel(uvX, uvY);
                    if (hitColor.a > 0.05)
                    {
                        Color pxColor = dataTexture.GetPixel(uvX, uvY);
                        if (pxColor.a > 0.05)
                        {
                            Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hitTransform.name + " IS NOT ALPHA | GOLPE CRITICO");
                        }
                        else
                        {
                            Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hitTransform.name + " IS NOT ALPHA");
                        }

                        //colliderTransform.parent = null;
                        colliderObj.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hitTransform.name + " IS ALPHA");
                    }
                }
            }
        }

        // Dibujar el raycast en la escena para depuración
        Debug.DrawRay(currentPosition, rayDirection * 7, Color.red);

        // Actualizar la posición y velocidad para el siguiente paso de tiempo
        currentPosition += currentVelocity * timeStep;
        currentVelocity += Vector3.up * gravity * timeStep;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("Activando Collider");
            colliderTransform.parent = null;
            colliderObj.SetActive(true);
        }
    }
    */
}
