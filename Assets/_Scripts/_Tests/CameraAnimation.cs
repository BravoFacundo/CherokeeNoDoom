using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAnimation : MonoBehaviour
{
    public Transform enemyTransform; // El transform del enemigo
    public float arcHeight = 2.0f; // Altura del arco
    public float duration = 2.0f; // Duraci�n de la animaci�n
    public float finalCameraHeight = 0.5f; // Altura final de la c�mara
    public Image imageToAnimate; // La imagen en el canvas
    public float imageMoveDistance = 100.0f; // Distancia de movimiento hacia abajo
    public Vector3 imageScaleFactor = new Vector3(1.2f, 1.2f, 1.0f); // Factor de escala de la imagen

    private Transform playerCamera; // El transform de la c�mara del jugador
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    private void Start()
    {
        playerCamera = Camera.main.transform;
        initialCameraPosition = playerCamera.position;
        initialCameraRotation = playerCamera.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerCamera.parent = null;
            StartCoroutine(MoveCameraAndAnimateImage());
        }
    }
    IEnumerator MoveCameraAndAnimateImage()
    {
        initialCameraRotation = playerCamera.rotation;
        float elapsedTime = 0.0f;
        Vector3 initialImagePosition = imageToAnimate.rectTransform.localPosition;
        Vector3 initialImageScale = imageToAnimate.rectTransform.localScale;
        bool imageAnimationStarted = false;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Calcula la posici�n de la c�mara en un arco sobre el enemigo
            Vector3 arcPosition = Vector3.Lerp(initialCameraPosition, enemyTransform.position, t);
            arcPosition.y += Mathf.Sin(t * Mathf.PI) * arcHeight;

            // Calcula la rotaci�n de la c�mara mirando cenitalmente al suelo
            Quaternion arcRotation = Quaternion.Slerp(initialCameraRotation, Quaternion.LookRotation(Vector3.down), t);

            playerCamera.position = arcPosition;
            playerCamera.rotation = arcRotation;

            // Aplica un retraso solo al movimiento hacia abajo de la imagen
            if (t > 1.0f / 6.0f && !imageAnimationStarted)
            {
                imageAnimationStarted = true;
                StartCoroutine(MoveImageDown());
            }

            // Anima la escala de la imagen
            imageToAnimate.rectTransform.localScale = Vector3.Lerp(initialImageScale, imageScaleFactor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ajusta la posici�n final de la c�mara
        playerCamera.position = enemyTransform.position + Vector3.up * finalCameraHeight;
        playerCamera.rotation = Quaternion.LookRotation(Vector3.down);
    }

    IEnumerator MoveImageDown()
    {
        float elapsedTime = 0.0f;
        Vector3 initialImagePosition = imageToAnimate.rectTransform.localPosition;

        while (elapsedTime < duration * (2.0f / 3.0f))
        {
            float t = elapsedTime / (duration * (2.0f / 3.0f));

            // Calcula la posici�n de la imagen hacia abajo
            Vector3 downPosition = initialImagePosition - Vector3.up * imageMoveDistance * t;
            imageToAnimate.rectTransform.localPosition = downPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegura que la imagen est� completamente abajo al final
        imageToAnimate.rectTransform.localPosition = initialImagePosition - Vector3.up * imageMoveDistance;
    }
}




