using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //gets the object the camera will focus on.
    [Header("Harness Object")]
    public Transform harnessFollow;

    //determines how quickly the camera will respond to the player's new position.
    [Range(0f, 10f)]
    [Header("Set Camera Speed")]
    public float setCameraSpeed;

    //how far away the camera will be from the player's ship.
    [Header("Offset Camera Setting")]
    public Vector3 cameraOffset;

    void LateUpdate()
    {
        //code will allow the camera to follow the blank game object.
        //reason being: the camera can stutter when they go between different lanes, so it is better to follow an object which will always be in lane 3.
        Vector3 desiredPosition = harnessFollow.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, setCameraSpeed);
        transform.position = smoothedPosition;
    }
}
