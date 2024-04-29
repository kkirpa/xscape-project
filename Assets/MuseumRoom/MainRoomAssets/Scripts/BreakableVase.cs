using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class BreakableVase : NetworkBehaviour
{
    public GameObject destroyedVersion;
    public GameObject statue;
    public AudioClip shatter;
    public GameObject keyPrefab;
    public GameObject marblePrefab;

    [ServerRpc(RequireOwnership = false)]
    public void UpdateServerRpc() 
    {
        // var position Vector3;
        BreakTheVaseRpc();
        PositionTransformRpc();
        Debug.Log("update rpc");
    }


    void Update(){
        // Debug.Log("normal update");
        UpdateServerRpc();
    }

    [Rpc(SendTo.Server)]
    public void PositionTransformRpc(){
         Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Debug.Log("update rpc");
        Debug.Log(position);
    }


    [Rpc(SendTo.Server)]
    public void BreakTheVaseRpc ()
    {
        var instance = Instantiate(destroyedVersion, transform.position, transform.rotation);
        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();
        
        if (shatter != null)
        {
            AudioSource.PlayClipAtPoint(shatter, transform.position);
        }

        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }

        if (marblePrefab != null)
        {
            Instantiate(marblePrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }

        //Destroy(gameObject);

        NetworkObject vaseNetworkObject = GetComponent<NetworkObject>();
        
        // Despawn the vase on all clients
        vaseNetworkObject.Despawn(true);
    }
}
