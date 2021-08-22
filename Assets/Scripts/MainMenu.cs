using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Manager.audio.Play("UIButtonOk");
        Manager.PlayBGMIfNotStartedYet();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Manager.audio.Play("UIButtonOk");
        Application.Quit();
    }

    public void BackToMenuButton()
    {
        Manager.audio.Play("UIButtonOk");
        SceneManager.LoadScene("MainMenu");
    }
}
