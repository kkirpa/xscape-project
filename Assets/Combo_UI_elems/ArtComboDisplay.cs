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

    public Animator _anim_fail;
    public Animator _anim_win;

    public AudioClip buttonClickSound;
    public AudioClip failSound;
    public AudioClip successSound;
    public AudioClip buttonNotActiveSound;
    public AudioSource audioSource;

    private int[] correctAnswer = {1, 2, 3};

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
        if (numCurrDisplayed == 0)
        {
            // audioSource.PlayOneShot(buttonClickSound);
            art_left.texture = myTextures[number];
            displayedIndices[0] = number;
            numCurrDisplayed++;
        }
        else if (numCurrDisplayed == 1)
        {
            // audioSource.PlayOneShot(buttonClickSound);
            art_mid.texture = myTextures[number];
            displayedIndices[1] = number;
            numCurrDisplayed++;
        }
        else if (numCurrDisplayed == 2)
        {
            // audioSource.PlayOneShot(buttonClickSound);
            art_right.texture = myTextures[number];
            displayedIndices[2] = number;
            numCurrDisplayed++;

            // now that the third frame has been selected,
            // check the key.
            bool isSuccess = true;
            for (int i = 0; i <= 2; i++){
                if (displayedIndices[i] != correctAnswer[i]){
                    isSuccess = false;
                }
            }
            if (isSuccess){
                // audioSource.PlayOneShot(successSound);
                WaitForAnimation(_anim_win);
                numCurrDisplayed = -1;
            }
            else{
                // audioSource.PlayOneShot(failSound);
                WaitForAnimation(_anim_fail);

                //reset all
                art_left.texture = myTextures[0];
                art_mid.texture = myTextures[0];
                art_right.texture = myTextures[0];
                displayedIndices[0] = 0;
                displayedIndices[1] = 0;
                displayedIndices[2] = 0;
                numCurrDisplayed = 0;
            }
        }
        else
        {
            // numCurrDisplayed is -1 meaning this is the correct
            // combo. The wall should not be updated anymore.

            // audioSource.PlayOneShot(buttonNotActiveSound);
            Debug.Log($"The buttons are not active anymore.");

        }
    }

    IEnumerator WaitForAnimation(Animator thisAnimator)
    {
        // Wait until the current state of the animation is not playing
        while (thisAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || thisAnimator.IsInTransition(0))
        {
            yield return null;
        }

        // Animation has finished, continue with the script
        Debug.Log("Animation finished, continue script here...");
    }

    public void correctEvent(){

    }

    public void incorrectEvent(){

    }
}
