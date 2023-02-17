using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cameraBounds : MonoBehaviour
{
    public Camera mainCamera;
    public float padding = 0.1f;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Calculate the boundaries of the camera
        float cameraDistance = Vector3.Distance(mainCamera.transform.position, transform.position);
        Vector2 cameraSize = new Vector2(mainCamera.aspect * cameraDistance, cameraDistance);
        minX = mainCamera.transform.position.x - cameraSize.x + padding;
        maxX = mainCamera.transform.position.x + cameraSize.x - padding;
        minY = mainCamera.transform.position.y - cameraSize.y + padding;
        maxY = mainCamera.transform.position.y + cameraSize.y - padding;
    }

    void LateUpdate()
    {
        // Keep the player object within the boundaries of the camera
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }
}
