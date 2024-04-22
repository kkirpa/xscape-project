using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class blinkingScript : MonoBehaviour
{

    public Material light_on;
    public Material light_off;
    public Renderer quadRenderer;

    public String year;



    // Start is called before the first frame update
    void Start()
    {
        // Start the blinking coroutine
        StartCoroutine(LoopBlink());
    }

    // Coroutine for blinking effect
    IEnumerator LoopBlink()
    {
        while (true)
        {
            // SOS code
            yield return StartCoroutine(Blink(1f));
            yield return StartCoroutine(Blink(1f));
            yield return StartCoroutine(Blink(1f));
            yield return StartCoroutine(Blink(0.5f));
            yield return StartCoroutine(Blink(0.5f));
            yield return StartCoroutine(Blink(0.5f));
            yield return StartCoroutine(Blink(1f));
            yield return StartCoroutine(Blink(1f));
            yield return StartCoroutine(Blink(1f));
            yield return StartCoroutine(Pause(1f));
        }
    }

    // funtion to blink light once for a specific amount of time
    IEnumerator Blink(float duration)
    {
        // Set material to light_on
        quadRenderer.material = light_on;
        // Wait for X second(s)
        yield return new WaitForSeconds(duration);

        // Set material to light_off
        quadRenderer.material = light_off;
        // Wait for 0.5 second
        yield return new WaitForSeconds(0.5f);
    }

    // funtion to maintain the state of the light for a duration
    IEnumerator Pause(float duration)
    {
        // Wait for 0.5 second
        yield return new WaitForSeconds(duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
