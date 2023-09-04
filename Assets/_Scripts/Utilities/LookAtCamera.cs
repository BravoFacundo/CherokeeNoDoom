using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool lockX = true;
    [SerializeField] private float maxRotationX = 0f;
    private Quaternion initialRotation;

    void Start()
    {
        //target = Camera.main.gameObject.transform;
        target = GameObject.Find("Player").transform;
        initialRotation = transform.rotation;
    }    

    void Update()
    {
        transform.LookAt(target);
        if (lockX)
        {
            var angleX = transform.eulerAngles.x;
            if (angleX >= maxRotationX && angleX < 360 - maxRotationX)
            {
                //print( "0 a 15 :" + angleX);
                angleX = Mathf.Clamp(angleX, 360 - maxRotationX, 360);
                transform.eulerAngles = new Vector3(angleX, transform.eulerAngles.y, 0);
            }
            /*
            else if (angleX >= 360 - maxRotationX && angleX <= 360)
            {
                print("345 a 360 :" + angleX);
                angleX = Mathf.Clamp(angleX, 0, maxRotationX);
                transform.eulerAngles = new Vector3(angleX, transform.eulerAngles.y, 0);
            }
            */
        }
    }
}
