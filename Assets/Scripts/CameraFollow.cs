using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset = new Vector3(0,0,-10);
    [Range(1, 10)]
    public float smoothFactor;

    public float minCameraY;

    private void FixedUpdate()
    {
        Vector3 smoothPosition = Vector3.Lerp(transform.position,followTarget.position+offset,smoothFactor*Time.fixedDeltaTime);
        Vector3 boundPosition = new Vector3(smoothPosition.x, Mathf.Clamp(smoothPosition.y, minCameraY, smoothPosition.y), smoothPosition.z);
        transform.position = boundPosition;
    }
}
