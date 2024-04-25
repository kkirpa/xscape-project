using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SuccessUI : MonoBehaviour
{
    //[SerializeField] GameObject _object;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void toLobby()
    {
        Debug.Log("button clicked");
        SceneManager.LoadScene("TutorialRoom");
    }

    // Update is called once per frame
    /*void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _object.transform.position);
    }*/
}
