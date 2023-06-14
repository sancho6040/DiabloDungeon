using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource, effectsSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void UpdateMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void PauseSounds()
    {
        musicSource.Pause();
        effectsSource.Pause();
    }

    public void UnPauseSounds()
    {
        musicSource.Pause();
        effectsSource.Pause();
    }

}
