using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour
{

    public GameObject redLight, redLight2;
    public float waitTime = .2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sirens());
    }

    IEnumerator Sirens()
    {
        yield return new WaitForSeconds(waitTime);

        redLight.SetActive(false);
        redLight2.SetActive(true);

        yield return new WaitForSeconds(waitTime);
        
        redLight.SetActive(true);
        redLight2.SetActive(false);

        StartCoroutine(Sirens());
    }
}
