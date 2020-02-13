using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource BallExplosion;
    public AudioSource BallCaptured;

    private void Awake()
    {
        Instance = this;
    }

    public enum SoundEffect
    {
        Explode,
        Capture
    }

    public void PlaySoundEffect(SoundEffect type)
    {
        switch(type)
        {
            case SoundEffect.Capture:
                BallCaptured.Play();
                break;
            case SoundEffect.Explode:
                BallExplosion.Play();
                break;
        }
    }
}
