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

    public Light l_left;
    public Light l_mid;
    public Light l_right;

    public int LIGHT_INTENSITY;

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

        l_left.intensity = 0f;
        l_mid.intensity = 0f;
        l_right.intensity = 0f;
        l_left.color = Color.white;
        l_mid.color = Color.white;
        l_right.color = Color.white;
    }

    public void ButtonClicked(int number)
    {
        Debug.Log($"Clicked {number}");
        if (numCurrDisplayed == 0)
        {
            audioSource.PlayOneShot(buttonClickSound);
            l_left.intensity = LIGHT_INTENSITY;
            art_left.texture = myTextures[number];
            displayedIndices[0] = number;
            numCurrDisplayed++;
        }
        else if (numCurrDisplayed == 1)
        {
            audioSource.PlayOneShot(buttonClickSound);
            l_mid.intensity = LIGHT_INTENSITY;
            art_mid.texture = myTextures[number];
            displayedIndices[1] = number;
            numCurrDisplayed++;
        }
        else if (numCurrDisplayed == 2)
        {
            audioSource.PlayOneShot(buttonClickSound);
            l_right.intensity = LIGHT_INTENSITY;
            art_right.texture = myTextures[number];
            displayedIndices[2] = number;
            numCurrDisplayed++;

            // now that the third frame has been selected,
            // check the key.
            CheckCombination();
        }
        else
        {
            // numCurrDisplayed is -1 meaning this is the correct
            // combo. The wall should not be updated anymore.

            audioSource.PlayOneShot(buttonNotActiveSound);
            Debug.Log($"The buttons are not active anymore.");
        }
    }

    private void CheckCombination(){
        bool isSuccess = true;
        for (int i = 0; i <= 2; i++){
            if (displayedIndices[i] != correctAnswer[i]){
                isSuccess = false;
            }
        }
        if (isSuccess){
            audioSource.PlayOneShot(successSound);
            numCurrDisplayed = -1;
            l_left.color = Color.green;
            l_mid.color = Color.green;
            l_right.color = Color.green;
            // TODO: trigger lasers
        }
        else{
            audioSource.PlayOneShot(failSound);
            StartCoroutine(ResetDisplayAfterDelay(2f));
        }
    }

    private IEnumerator ResetDisplayAfterDelay(float delay)
    {
        l_left.intensity = LIGHT_INTENSITY * 2;
        l_mid.intensity = LIGHT_INTENSITY * 2;
        l_right.intensity = LIGHT_INTENSITY * 2;
        l_left.color = Color.red;
        l_mid.color = Color.red;
        l_right.color = Color.red;

        // Wait for delay
        yield return new WaitForSeconds(delay);

        //reset all
        art_left.texture = myTextures[0];
        art_mid.texture = myTextures[0];
        art_right.texture = myTextures[0];
        displayedIndices[0] = 0;
        displayedIndices[1] = 0;
        displayedIndices[2] = 0;
        numCurrDisplayed = 0;
        l_left.intensity = 0f;
        l_mid.intensity = 0f;
        l_right.intensity = 0f;
        l_left.color = Color.white;
        l_mid.color = Color.white;
        l_right.color = Color.white;
    }

    IEnumerator Pause(float duration)
    {
        // Wait for X seconds
        yield return new WaitForSeconds(duration);
    }

    public void correctEvent(){

    }

    public void incorrectEvent(){

    }
}
