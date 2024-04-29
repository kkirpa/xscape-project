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
        SceneManager.LoadScene("Scenes/TutorialRoom");
    }

    public void toMain()
    {
        SceneManager.LoadScene("Scenes/TestScene");
    }

    public void toVault()
    {
        SceneManager.LoadScene("Scenes/VaultRoomScene");
    }

    // Update is called once per frame
    /*void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _object.transform.position);
    }*/
}
