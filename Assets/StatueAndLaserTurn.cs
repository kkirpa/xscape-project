using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StatueAndLaserTurn : MonoBehaviour
{
    public GameObject rightLaser;
    public GameObject leftLaser;
    public GameObject statue;

    public GameObject glassbox;

    public GameObject crown;
    private XRGrabInteractable crownGrabInteractable;

    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        crownGrabInteractable = crown.GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void turnStatue()
    {
            if (anim != null)
            {
                anim.Play("Statue", 0, 0.25f);
                Invoke("turnOnLasers", 1);
            }        
    }

    public void turnOnLasers()
    {
        rightLaser.SetActive(true);
        leftLaser.SetActive(true);

        glassbox.SetActive(false);
        if (crownGrabInteractable != null)
        {
            crownGrabInteractable.enabled = true;
        }
    }
}
