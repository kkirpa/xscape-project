using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueAndLaserTurn : MonoBehaviour
{
    public GameObject rightLaser;
    public GameObject leftLaser;
    public GameObject statue;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
    }
}
