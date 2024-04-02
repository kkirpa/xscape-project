using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueMovement : MonoBehaviour
{
    public float rotationSpeed = 0.5f; // Speed of rotation

    private static bool isRotating = false;

    public AudioClip stoneSound;

    // Method to start the rotation of the statue
    public void StartRotation()
    {
        isRotating = true;
        if (stoneSound != null)
        {
            AudioSource.PlayClipAtPoint(stoneSound, transform.position);
        }
        
    }

    void Update()
    {
        // Check if the statue is currently rotating
        if (isRotating)
        {
            // Rotate the statue around its own center
            transform.Rotate(0, rotationSpeed, 0);
        }
    }
}


