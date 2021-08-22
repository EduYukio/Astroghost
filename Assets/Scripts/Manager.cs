using UnityEngine;

public static class Manager
{
    public static AudioManager audio { get; private set; }
    public static bool BGMStarted = false;

    static Manager()
    {
        GameObject preloadObj = GameObject.Find("PreloadObject");

        audio = preloadObj.GetComponent<AudioManager>();
    }

    public static void PlayBGMIfNotStartedYet()
    {
        if (!BGMStarted)
        {
            audio.Play("BGM");
            BGMStarted = true;
        }
    }
}