using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float secondsToChangeScene = 5f;
    void Start()
    {
        Invoke(nameof(ChangeSceneAfterSeconds), secondsToChangeScene);
    }

    void Update()
    {

    }

    private void ChangeSceneAfterSeconds()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
