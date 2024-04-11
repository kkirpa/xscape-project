using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;
    
    public void ToggleDoorState() {
        doorAnim = gameObject.GetComponent<Animator>();
        if( !doorOpen ) {
        doorAnim.Play( "OpenDoor" );
        } else {
        doorAnim.Play( "CloseDoor" );
        }
        doorOpen = !doorOpen;
    }
}
