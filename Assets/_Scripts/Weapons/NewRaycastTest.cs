using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRaycastTest : MonoBehaviour
{
    private GameObject colliderObj;
    private Transform colliderTransform;

    public float initialSpeed = 110f;
    private float gravity = -9.81f;
    private Rigidbody rb;

    private bool hitOccurred = false;
    private RaycastHit[] hits; // Almacenará todos los objetos alcanzados
    private List<Collider> ignoredColliders = new List<Collider>();

    private void Awake()
    {
        colliderObj = transform.GetChild(0).gameObject;
        colliderTransform = colliderObj.transform;
        colliderObj.SetActive(false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * initialSpeed;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = rb.velocity;
        float timeStep = Time.deltaTime;

        // Aca seria mejor predeterminar la cantidad de distancia que va a avanzar.
        RaycastHit[] allHits = Physics.RaycastAll(currentPosition, currentVelocity.normalized, 7f);
        hits = allHits;

        foreach (RaycastHit hit in allHits)
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
                        //print(hit.transform.name);
                        //hitOccurred = true; // Indicar que un hit válido ocurrió
                        //break; // Salir del bucle al encontrar un hit válido

                        foreach (Collider ignoredCollider in ignoredColliders)
                        {
                            Physics.IgnoreCollision(colliderObj.GetComponent<Collider>(), ignoredCollider, false);
                        }
                        ignoredColliders.Clear();
                        colliderObj.SetActive(true);
                        //print(hit.transform.name);

                        //hit.transform.parent.gameObject.SetActive(false);
                        //gameObject.SetActive(false);
                        transform.position = hit.point;
                        GetComponent<Rigidbody>().isKinematic = true;

                        break;
                    }
                }
            }
        }

        if (hitOccurred)
        {
            // Realizar la operación si hitColor.a > 0.05 en el primer objeto alcanzado
            // Resto del código relacionado con el hit válido
            // ...

            // Activar el objeto de colisión
            colliderObj.SetActive(true);
        }

        // Dibujar el raycast en la escena para depuración
        Debug.DrawRay(currentPosition, currentVelocity.normalized * 7, Color.red);

        currentPosition += currentVelocity * timeStep;
        currentVelocity += Vector3.up * gravity * timeStep;
    }
}
