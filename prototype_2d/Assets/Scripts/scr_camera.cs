using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_camera : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;
    public float smoothSpeed = 0.125f;
    public PlayerController playerScript;

    public Vector3 mousePos;

    public Vector3 smoothedPosition;
    public Vector3 desiredPosition;
    public float distance;
    public float maxDistance;

    public GameObject testObjectDesirePOS;//test cube

    Vector3 _velocity;


    void LateUpdate()
    {
        // The line below may cause a feedback loop
        testObjectDesirePOS.transform.position = (player.transform.position + mousePos) / 2;

        mousePos = playerScript.testObject.transform.position;

        distance = Vector3.Distance(player.position, mousePos);

        desiredPosition = ((player.transform.position + testObjectDesirePOS.transform.position) / 2) + cameraOffset;

        //smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //Changed to use smoothdamp
        smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, 0.5f);
        transform.position = smoothedPosition;

        transform.LookAt(smoothedPosition);
    }
}
