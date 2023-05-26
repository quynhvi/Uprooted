using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The character's transform to follow
    public float smoothSpeed = 0.125f;  // The smoothing factor for camera movement
    public Vector3 offset;  // The offset between the character and the camera
    public float minCameraX = -8.29f;  // Min/Max x position the camera can have
    public float maxCameraX = 28.25f;  

    private Vector3 desiredPosition;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        float targetX = Mathf.Clamp(target.position.x, minCameraX, maxCameraX);
        desiredPosition = new Vector3(targetX, transform.position.y, transform.position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
