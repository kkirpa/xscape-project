// using System;
// using Unity.Netcode;
// using UnityEngine;

// public class NetworkTransformTest : NetworkBehaviour
// {
//     void Update()
//     {
//         if (IsServer)
//         {
//             //float theta = Time.frameCount / 10.0f;
//             transform.position = new Vector3(0, 1, 2);
//         }
//     }
// }

using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
{

    public GameObject cam; 
    void Start()
    {
        // Find the player GameObject dynamically at runtime
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        // Check if the player GameObject is found
        if (cam != null)
        {
            // Update the position of the networked GameObject to match the player's position
            transform.position = cam.transform.position;
            transform.rotation = cam.transform.rotation;
        }
        else
        {Debug.LogWarning("cam GameObject not found.");
            
        }
    }
}
