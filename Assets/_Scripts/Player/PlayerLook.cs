using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    float multiplier = 0.01f;    
    float mouseX;
    float mouseY;
    float xRotation;
    float yRotation;
    
    [Header("References")]
    [SerializeField] Transform orientationObject;
    Camera cam;

    [Header("PlayerDie")]
    [HideInInspector] public Transform lookTarget;
    [HideInInspector] public bool readyToLookTarget = false;
    public float totalRotationTime = 1f;  // Total time for rotation
    public AnimationCurve rotationCurve = AnimationCurve.Linear(0, 0, 1, 1);  // Animation curve for acceleration/deceleration

    private float elapsedTime = 0f;
    float rotationSpeed;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!readyToLookTarget)
        {
            MoveInput();

            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientationObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            cam.transform.LookAt(lookTarget);
        }
    }

    void MoveInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
