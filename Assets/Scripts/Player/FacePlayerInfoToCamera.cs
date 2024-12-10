using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTagToVRCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _cam; // Assign the VR camera manually or dynamically

    void Start()
    {
        // If no camera is assigned, try to find the main camera
        if (_cam == null)
        {
            _cam = Camera.main;
        }
    }

    void Update()
    {
        if (_cam != null)
        {
            // Calculate the direction to the VR camera
            Vector3 direction = _cam.transform.position - transform.position;
            direction.y = 0; // Ignore Y-axis rotation for horizontal alignment

            // Rotate to face the VR camera only on the Y-axis
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
