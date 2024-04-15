using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArtButtonScript : MonoBehaviour
{

    public int artworkNumber = 1;

    public UnityEvent artworkClicked;
    
    private void OnMouseDown()
    {
        artworkClicked.Invoke();
    }
}
