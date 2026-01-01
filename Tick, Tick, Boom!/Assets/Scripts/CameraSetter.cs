using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField, Range(0f, 1f)] private float movementSmooth = 0.5f;
    [SerializeField, Range(0f, 1f)] private float sizeSmooth = 0.2f;

    [SerializeField] Vector3 startingCameraPosition;
    [SerializeField] float startingCameraSize;

    private Vector3 newCameraPosition;
    private float newCameraSize;

    private void Awake()
    { 
        mainCamera = GetComponent<Camera>();

        newCameraPosition = startingCameraPosition;
        newCameraSize = startingCameraSize;
    }

    private void Update()
    {
        float smoothedX = Mathf.Lerp(transform.position.x, newCameraPosition.x, movementSmooth);
        float smoothedY = Mathf.Lerp(transform.position.y, newCameraPosition.y, movementSmooth);

        Vector3 smoothedPosition = new Vector3(smoothedX, smoothedY, newCameraPosition.z);

        gameObject.transform.position = smoothedPosition;

        float smoothedSize = Mathf.Lerp(mainCamera.orthographicSize, newCameraSize, sizeSmooth);
        mainCamera.orthographicSize = newCameraSize;
    }

    private void SetCamera(Vector3 newCameraPosition, float newCameraSize, Vector3 nonUsedVector3)
    {
        this.newCameraPosition = newCameraPosition;
        this.newCameraSize = newCameraSize;
    }

    private void OnEnable()
    {
        GameEventsController.RoomFinished += SetCamera;
    }
    private void OnDisable()
    {
        GameEventsController.RoomFinished -= SetCamera;
    }
}
