using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortal : MonoBehaviour
{
    public bool playSFX = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel()
    {
        if (playSFX) Manager.audio.Play("NextLevel");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
