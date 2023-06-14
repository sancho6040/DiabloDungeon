using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioClip buttonClip, musicClip;
    private void Start()
    {
        SoundManager.Instance.PlayMusic(musicClip);
        volumeSlider.onValueChanged.AddListener(val => SoundManager.Instance.UpdateMasterVolume(val));
    }

    public void StartGame()
    {
        PlayButtonSound();
        Invoke("GotToGame", 0.3f);
    }

    void GotToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GotToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        SoundManager.Instance.PauseSounds();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        SoundManager.Instance.UnPauseSounds();
    }

    void PlayButtonSound()
    {
        SoundManager.Instance.PlaySound(buttonClip);
    }

}
