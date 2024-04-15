using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Keypad : MonoBehaviour
{
    public string passcode = "0000";
    private string userInput = "";
    //public Text UIText = null;
    
    private void Start()
    {
        userInput = "";
    }
    
    public void ButtonClicked(string number)
    {
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
                Debug.Log("Entry approved");
                userInput = "";
                // TODO: event to open safe (including safe opening sound)
            }
            else
            {
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
