using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class DeathMenu : MonoBehaviour
{
    //drag audio source here
    public AudioSource buttonClickSound;

    public void Respawn()
    {
        // starts routine for sound
        StartCoroutine(PlaySoundAndLoad());
    }

    IEnumerator PlaySoundAndLoad()
    {
        // if there is sound play it
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();

            //waits till sound is done before loading
            yield return new WaitForSeconds(buttonClickSound.clip.length);
        }

        // loads new scene
        SceneManager.LoadScene("Test 1");
    }
}