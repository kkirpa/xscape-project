using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class StartClient : MonoBehaviour
{
    private NetworkManager netManager;

    // Start is called before the first frame update

    // void Start()
    // {
    //     netManager = GetComponentInParent<NetworkManager>();
    // }

    public void buttonClick()
    {
        Debug.Log("client started");
        netManager = GetComponentInParent<NetworkManager>();
        netManager.StartClient();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
