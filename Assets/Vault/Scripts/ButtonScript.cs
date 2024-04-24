using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{

    public int keypadNumber = 0;
    public UnityEvent KeypadClicked;

    private void OnMouseDown()
    {
        KeypadClicked.Invoke();
    }
}
