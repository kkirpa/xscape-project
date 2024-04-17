using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeOpen : MonoBehaviour
{
    //reference to the animator component of the safe
    [SerializeField]
    public Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //trigger - space key for now
        
            //_anim.SetTrigger("0000");
        
    }

    // private void OnMouseDown()
    // {
    //     _anim.SetTrigger("0000");
    // }
}
