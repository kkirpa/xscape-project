using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class BreakableVase : MonoBehaviour
{
    public GameObject destroyedVersion;
    public GameObject statue;
    public AudioClip shatter;
    public GameObject keyPrefab;
    public GameObject marblePrefab;
    
    public void BreakTheVase ()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);

        
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

        Destroy(gameObject);
    }
}
