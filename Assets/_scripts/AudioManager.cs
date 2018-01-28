using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioMixerSnapshot NormalSpeed;
    public AudioMixerSnapshot GameOver;
    public AudioMixerSnapshot SpeedUp;
    public AudioSource explosion;

    static AudioSource explosionSound;

    // Use this for initialization
    void Start () {
        GamePlayManager.OnGameStart += OnGameStart;
        GamePlayManager.OnGameOver += OnGameOver;
        GamePlayManager.OnBackToMainMenu += OnBackToMainMenu;
        explosionSound = explosion;
    }

    void OnGameStart()
    {
        NormalSpeed.TransitionTo(0.01f);

    }
    void OnGameOver()
    {
        GameOver.TransitionTo(0.01f);
    }

    void OnBackToMainMenu()
    {
        NormalSpeed.TransitionTo(0.01f);
    }

    public static void PlayExplosion()
    {
        explosionSound.Play();
    }


}
