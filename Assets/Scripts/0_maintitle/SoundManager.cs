using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;

    public void SetMusic(float volume)
    {
        music.volume = volume;
    }
}
