using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    public bool zoomActive;
    public Vector3[] target;
    public CinemachineVirtualCamera virtualCamera;
    public float speed;
    public float zoomOutDuration = 4f; // Duration in seconds for zoom out

    private float zoomOutTimer; // Timer for tracking zoom out duration

    public InputActionReference zoomInAction;
    public InputActionReference zoomOutAction;

    public void OnEnable()
    {
        zoomInAction.action.Enable();
        zoomOutAction.action.Enable();
    }

    public void OnDisable()
    {
        zoomInAction.action.Disable();
        zoomOutAction.action.Disable();
    }

    private void Start()
    {
        zoomActive = true;
        zoomOutTimer = 0f;
    }

    void Update()
    {
        if (zoomInAction.action.triggered)
        {
            zoomActive = true; // Zoom in
            zoomOutTimer = 0f; // Reset the zoom out timer
        }
        else if (zoomOutAction.action.triggered)
        {
            zoomActive = false; // Zoom out
            zoomOutTimer = 0f; // Reset the zoom out timer
        }

        if (!zoomActive)
        {
            zoomOutTimer += Time.deltaTime;
            if (zoomOutTimer >= zoomOutDuration)
            {
                zoomActive = true; // Automatically trigger zoom in
            }
        }
    }

    public void LateUpdate()
    {
        if (zoomActive)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 1.3f, speed);
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, target[1], speed);
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 4.9f, speed);
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, target[0], speed);
        }
    }
}
