using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float openAngle = 90f; // Adjust as needed
    public float closeAngle = 0f; // Adjust as needed
    public float rotationSpeed = 90f; // Adjust as needed
    public Transform hingePoint; // The point around which the door rotates

    private static bool isOpen = false;

    // Method to rotate the door towards the target rotation
    public void RotateDoor(bool open)
    {
        // Calculate the target rotation
        float targetAngle = open ? openAngle : closeAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        // Rotate the door towards the target rotation
        if (hingePoint != null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            isOpen = open; // Update the door state
        }
        else
        {
            Debug.LogError("Hinge point not assigned for the door!");
        }
    }
}
