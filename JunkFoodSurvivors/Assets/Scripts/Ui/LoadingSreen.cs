using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LoadingScreen : MonoBehaviour
{
    public float WaitTime = 4.0f;
    public string LoadToScene = "Test 1";

    void Start()
    {
        StartCoroutine(WachtEnLaadScene());
    }

    IEnumerator WachtEnLaadScene()
    {
        yield return new WaitForSeconds(WaitTime);

        SceneManager.LoadScene(LoadToScene);
    }
}