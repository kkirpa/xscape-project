using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStatue : MonoBehaviour
{
    public GameObject statue; // Assign the statue GameObject in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) // Ensure the collider is triggered by the main camera
        {
            // Rotate the statue to face the direction of the cube's up vector
            statue.transform.LookAt(transform.position + transform.up, Vector3.up);
        }
    }
}
