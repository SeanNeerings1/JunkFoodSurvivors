using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; //neede to reload scenes

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource backgroundSource;
    public AudioSource bossSource;

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f;

    private float originalBackgroundVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //saves all the music so we can reset
            if (backgroundSource != null) originalBackgroundVolume = backgroundSource.volume;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        // tells unity that we want to listen  to scenes changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        // makes sure it disappears when scenes loaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //loads the music again
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetToNormalMusic();
    }
    private void ResetToNormalMusic()
    {
       //stops the fading else it sounds weird
        StopAllCoroutines();

        //bye bye bosic music
        if (bossSource != null)
        {
            bossSource.Stop();
        }

        // starts from beginning
        if (backgroundSource != null)
        {
            backgroundSource.Stop(); 
            backgroundSource.volume = originalBackgroundVolume;
            backgroundSource.Play();
        }

        Debug.Log("AudioManager: scene reloaded music restarts");
    }
    public void SwitchToBossMusic()
    {
        StartCoroutine(FadeOutAndPlayBoss());
    }
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