using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Respawn()
    {
        SceneManager.LoadScene("Test 1");
    }
}