using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exists because there is a "jittering" in the camera when is moving.
//This solution involves constantly teleporting the object holding the camera to the appropriate position on the player.

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    private void Start() => transform.parent = null;
    private void Update() => transform.position = cameraPosition.position;

}

//Here is a list of suggested solutions to solve it without creating this script:
// - Use LateUpdate();
// - Add Physics.SyncTransforms(); after changing transform.
// - Replace transform.rotation with GetComponent<Rigidbody>().rotation.
//I tried all the solutions and it didn't work.
