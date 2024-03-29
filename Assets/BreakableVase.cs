using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakableVase : MonoBehaviour
{
    public GameObject destroyedVersion;

    void Update() {
        
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // Check if the ray hits the vase
            if (Physics.Raycast(ray, out hit))
            {
                // Print the name of the object that was hit
                Debug.Log("Object Clicked: " + hit.collider.gameObject.name);

                // Check if the object hit is the vase
                if (hit.collider.gameObject == gameObject)
                {
                    BreakTheVase();
                }
            }

        }
    }
    public void BreakTheVase ()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
