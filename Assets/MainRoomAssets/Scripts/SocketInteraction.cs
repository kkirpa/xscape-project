using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteraction : MonoBehaviour
{
    public GameObject objectToRotate; // The GameObject to rotate when the specific grabbable object is placed in the socket
    public string specificGrabbableTag = "SpecificGrabbable"; // Tag of the specific grabbable object

    private XRSocketInteractor socketInteractor;

    void Start()
    {
        // Get reference to the XRSocketInteractor component
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Subscribe to the OnSelectEnter event
        if (socketInteractor != null)
        {
            socketInteractor.onSelectEntered.AddListener(OnSelectEnter);
        }
    }

    void OnSelectEnter(XRBaseInteractable interactable)
    {
        // Check if the grabbed object has the specific grabbable tag
        if (interactable.CompareTag(specificGrabbableTag))
        {
            // Rotate the specified GameObject
            RotateObject();
        }
    }

    void RotateObject()
    {
        // Example: Rotate the objectToRotate by 90 degrees around the y-axis
        objectToRotate.transform.Rotate(Vector3.up, 90f);
    }
}
