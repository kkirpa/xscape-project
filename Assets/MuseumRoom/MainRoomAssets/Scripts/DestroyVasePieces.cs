using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVasePieces : MonoBehaviour
{
    public GameObject vasePieces;

    void Update() {
        Destroy(vasePieces, 5);
    }
}
