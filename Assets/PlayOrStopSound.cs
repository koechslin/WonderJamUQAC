using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOrStopSound : MonoBehaviour
{
    public void PlaySound(string soundName)
    {
        FindObjectOfType<AudioManager>().Play(soundName);
    }

    public void StopSound(string soundName)
    {
        FindObjectOfType<AudioManager>().Stop(soundName);
    }
}
