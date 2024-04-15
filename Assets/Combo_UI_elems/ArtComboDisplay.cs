using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArtComboDisplay : MonoBehaviour
{
    public RawImage art_left;
    public RawImage art_mid;
    public RawImage art_right;
    private RawImage[] displays = new RawImage[3];

    private int[] displayedIndices = {0, 0, 0};
    private int numCurrDisplayed = 0;
    public Texture[] myTextures = new Texture[17];

    // called before first frame
    void Start()
    {
        displays[0] = art_left;
        displays[1] = art_mid;
        displays[2] = art_right;

        art_left.texture = myTextures[0];
        art_mid.texture = myTextures[0];
        art_right.texture = myTextures[0];
    }

    public void ButtonClicked(int number)
    {
        Debug.Log($"Clicked {number}");
        int artToUpdate = numCurrDisplayed;
        if (artToUpdate == 0)
        {
            art_left.texture = myTextures[number];
            numCurrDisplayed++;
        }
        else if (artToUpdate == 1)
        {
            art_mid.texture = myTextures[number];
            numCurrDisplayed++;
        }
        else if (artToUpdate == 2)
        {
            art_right.texture = myTextures[number];
            numCurrDisplayed++;
        }
        else
        {
            //reset all
            art_left.texture = myTextures[0];
            art_mid.texture = myTextures[0];
            art_right.texture = myTextures[0];
            numCurrDisplayed = 0;
        }
        if (numCurrDisplayed == 2){
            // here we will check the combo to see if it is right or wrong
            // then reset it
            Debug.Log("now check combo.");
        }
    }
}
