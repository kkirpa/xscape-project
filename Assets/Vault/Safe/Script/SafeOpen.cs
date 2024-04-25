using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeOpen : MonoBehaviour
{
    //reference to the animator component of the safe
    [SerializeField]
    public Animator _anim;

    public GameObject handleObject;
    public AudioSource audHandle;

    public GameObject doorObject;
    public AudioSource audDoor;

    public GameObject moneyRainBack;
    public AudioSource audMoneyBack;
    public ParticleSystem psBack;

    public GameObject moneyRainFront;
    public AudioSource audMoneyFront;
    public ParticleSystem psFront;

    public GameObject sirenObject1;
    public AudioSource audsiren1;

    public GameObject sirenObject2;
    public AudioSource audsiren2;

    public GameObject sirenObject3;
    public AudioSource audsiren3;

    public GameObject sirenObject4;
    public AudioSource audsiren4;

    public Siren siren1;
    public Siren siren2;
    public Siren siren3;
    public Siren siren4;

    public GameObject successUI1;
    public GameObject successUI2;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        audHandle = handleObject.GetComponent<AudioSource>();
        audDoor = doorObject.GetComponent<AudioSource>();

        psBack = moneyRainBack.GetComponent<ParticleSystem>();
        audMoneyBack = moneyRainBack.GetComponent<AudioSource>();

        psFront = moneyRainFront.GetComponent<ParticleSystem>();
        audMoneyFront = moneyRainFront.GetComponent<AudioSource>();
        
        audsiren1 = sirenObject1.GetComponent<AudioSource>();
        audsiren2 = sirenObject2.GetComponent<AudioSource>();
        audsiren3 = sirenObject3.GetComponent<AudioSource>();
        audsiren4 = sirenObject4.GetComponent<AudioSource>();

        siren1 = sirenObject1.GetComponent<Siren>();
        siren2 = sirenObject2.GetComponent<Siren>();
        siren3 = sirenObject3.GetComponent<Siren>();
        siren4 = sirenObject4.GetComponent<Siren>();

        successUI1.SetActive(false);
        successUI2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //trigger - space key for now
        
            //_anim.SetTrigger("0000");
        
    }

    public void play_handleSound()
    {
        audHandle.Play();
    }

    public void play_doorSound()
    {
        audDoor.Play();
    }

    public void start_rainningMoney()
    {
        psBack.Play();
        audMoneyBack.Play();

        psFront.Play();
        audMoneyFront.Play();
    }

    public void play_sirenSound()
    {
        audsiren1.Play();
        audsiren2.Play();
        audsiren3.Play();
        audsiren4.Play();

        siren1.begin();
        siren2.begin();
        siren3.begin();
        siren4.begin();
    }

    public void activate_screen()
    {
        successUI1.SetActive(true);
        successUI2.SetActive(true);
    }

}
