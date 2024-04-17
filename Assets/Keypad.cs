using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    public string passcode = "0000";
    private string userInput = "";
    public TextMeshPro UIText;

    public AudioClip clickSound;
    public AudioClip successSound;
    public AudioClip failSound;
    AudioSource audioSource;

    private void Start()
    {
        userInput = "";
        audioSource = GetComponent<AudioSource>();
        //UIText = GetComponent<TMPro.TextMeshProUGUI>();
        // UIText = GetComponent<TextMeshPro>();
        UIText = GetComponentInChildren<TextMeshPro>();
        UIText.text = "hi";
    }
    
    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        if(number == "-1" & userInput.Length >= 0)
        {
            if(userInput.Length != 0)
            {
                userInput = userInput.Remove(userInput.Length - 1);
                Debug.Log("Removal: " + userInput);
                
            }
            else
            {
                Debug.Log("Empty string: " + userInput + ", string length = " + userInput.Length);
            }

        }
        else if(number == "-2")
        {
            if(userInput == passcode)
            {
                audioSource.PlayOneShot(successSound);
                Debug.Log("Entry approved");
                userInput = "";
                // TODO: event to open safe (including safe opening sound)
            }
            else
            {
                audioSource.PlayOneShot(failSound);
                Debug.Log("Entry denied");
                userInput = "";
            }
        }
        else
        {
            userInput += number;
            Debug.Log(userInput);
        }
        


    }
}
