using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 7f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}