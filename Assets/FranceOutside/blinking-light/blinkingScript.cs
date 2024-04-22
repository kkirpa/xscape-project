using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkingScript : MonoBehaviour
{

    public Material light_on;
    public Material light_off;
    public Renderer quadRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Start the blinking coroutine
        StartCoroutine(Blink());
    }

    // Coroutine for blinking effect
    IEnumerator Blink()
    {
        while (true)
        {
            // Set material to light_on
            quadRenderer.material = light_on;
            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Set material to light_off
            quadRenderer.material = light_off;
            // Wait for 1 second
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
