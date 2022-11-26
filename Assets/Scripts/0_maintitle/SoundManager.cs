using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource musicaudio;  //배경음악
    public AudioSource effectaudio; //효과음

    public AudioClip clickSfx;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetMusic(float volume)
    {
        musicaudio.volume = volume;
    }

    public void SetEffect(float volume)
    {
        effectaudio.volume = volume;
    }

    public void PlayEffectSound(string name)
    {
        effectaudio.PlayOneShot(Resources.Load<AudioClip>(name));
    }
}
