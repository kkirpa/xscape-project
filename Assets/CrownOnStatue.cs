using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownOnStatue : MonoBehaviour
{
    [SerializeField]
    public GameObject NextRoom;
    public CrownOnStatue crown;

    // Start is called before the first frame update
    void Start()
    {
        // transform.position = new Vector3(-9.56570053f,4.14979601f,-9.49598885f);
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit))
    {
        if (hit.collider)
        {
            if (hit.transform.CompareTag("StatueHead"))
            {
                transform.position = new Vector3(-9.56570053f,4.14979601f,-9.49598885f);
                // transform.rotation = new Quaternion(0.000129200009f,277.367615f,3.73256153e-05f);
                GetComponent<Rigidbody>().mass = 0.0f;
                NextRoom.SetActive(true);
            }
        }
    }
        
    }
}
