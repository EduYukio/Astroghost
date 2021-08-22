using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
