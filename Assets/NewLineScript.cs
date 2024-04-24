using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLineScript : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField]
    public Transform startPoint;
    public BreakableWindow glassCase;
    public GameObject laser;
    public GameObject rightWall;
    private bool isActive = true;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
{
    lr.SetPosition(0, startPoint.position);
    RaycastHit hit;
    if (Physics.Raycast(transform.position, -transform.right, out hit))
    {
        if (hit.collider)
        {
            lr.SetPosition(1, hit.point); 
            if (hit.transform.CompareTag("BreakableGlass"))
            {
                glassCase.breakWindow();
                rightWall.SetActive(true);
            }
        }
    }
    else
    {

        lr.SetPosition(1, transform.position - transform.right * 5000);
    }
}

}
