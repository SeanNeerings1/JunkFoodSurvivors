using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource backgroundSource;
    public AudioSource bossSource;

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f; 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchToBossMusic()
    {
        StartCoroutine(FadeOutAndPlayBoss());
    }
//this makes it fade out
    private IEnumerator FadeOutAndPlayBoss()
    {
        if (backgroundSource.isPlaying)
        {
            float startVolume = backgroundSource.volume;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                backgroundSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
                yield return null;
            }

            backgroundSource.Stop();
            backgroundSource.volume = startVolume;
        }

        if (!bossSource.isPlaying)
        {
            bossSource.Play();
            Debug.Log("AudioManager: Boss has appeared");
        }
    }

    public void SwitchToNormalMusic()
    {
        if (bossSource.isPlaying)
        {
            bossSource.Stop();
        }

        if (!backgroundSource.isPlaying)
        {
            backgroundSource.Play();
        }
    }
}