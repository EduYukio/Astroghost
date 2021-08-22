using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congratulations : MonoBehaviour
{
    void Start()
    {
        Sound BGM = Manager.audio.FindSound("BGM");
        BGM.volume = 0f;
        Manager.audio.SetSourceSettings(BGM);
        Manager.audio.Play("Victory");
        Invoke(nameof(ReturnBGMVolume), 4.5f);
    }

    void Update()
    {

    }

    public void ReturnBGMVolume()
    {
        Sound BGM = Manager.audio.FindSound("BGM");
        BGM.volume = 0.45f;
        Manager.audio.SetSourceSettings(BGM);
    }
}
