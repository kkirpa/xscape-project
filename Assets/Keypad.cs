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
        UIText = GetComponentInChildren<TextMeshPro>();
    }
    
    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        if(number == "-1" & userInput.Length >= 0)
        {
            if(userInput.Length != 0)
            {
                userInput = userInput.Remove(userInput.Length - 1);
                
            }
            /*else
            {
                Debug.Log("Empty string: " + userInput + ", string length = " + userInput.Length);
            }*/
            UIText.text = userInput;
        }
        else if(number == "-2")
        {
            if(userInput == passcode)
            {
                audioSource.PlayOneShot(successSound);
                userInput = "";
                UIText.text = "SUCCESS";
            }
            else
            {
                audioSource.PlayOneShot(failSound);
                userInput = "";
                UIText.text = "TRY AGAIN";
            }
        }
        else
        {
            if(userInput.Length < 4)
            {
                userInput += number;
                Debug.Log(userInput);
                UIText.text = userInput;
            }
        }
    }
}
