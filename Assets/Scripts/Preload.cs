using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (PreloadInitializer.selectedScene > 0)
        {
            SceneManager.LoadScene(PreloadInitializer.selectedScene);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
