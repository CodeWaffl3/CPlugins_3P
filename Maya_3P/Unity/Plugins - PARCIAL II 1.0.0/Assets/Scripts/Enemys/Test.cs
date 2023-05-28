using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Test : MonoBehaviour
{
    // Adjust the speed for the application.
    public float movementSpeed = 1.0f;

    // The target (cylinder) position.
    private Transform target;

    void Awake()
    {
        // Position the cube at the origin.
        //transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        // Grab cylinder values and place on the target.
        target.transform.position = new Vector3(0.8f, 10.0f, 0.8f);

        // Position the camera.
        // Camera.main.transform.position = new Vector3(0.85f, 1.0f, -3.0f);
        // Camera.main.transform.localEulerAngles = new Vector3(15.0f, -20.0f, -0.5f);
        
    }

    void Update()
    {
        // Move our position a step closer to the target.
        
        
    }
}
