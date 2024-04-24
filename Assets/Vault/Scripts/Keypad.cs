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
    public GameObject safeObject;
    public Animator _anim;

    public AudioClip clickSound;
    public AudioClip successSound;
    public AudioClip failSound;
    AudioSource audioSource;

    private void Start()
    {
        userInput = "";
        audioSource = GetComponent<AudioSource>();
        UIText = GetComponentInChildren<TextMeshPro>();
        _anim = safeObject.GetComponent<Animator>();
        Debug.Log("_anim: " + _anim);
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
            UIText.text = userInput;
        }
        else if(number == "-2")
        {
            if(userInput == passcode)
            {
                audioSource.PlayOneShot(successSound);
                userInput = "";
                UIText.text = "SUCCESS";
                // TODO: Create event trigger
                _anim.Play("KeypadCorrect");
                _anim.SetTrigger("0000");
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
