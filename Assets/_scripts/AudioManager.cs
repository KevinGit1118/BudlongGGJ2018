using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioMixerSnapshot NormalSpeed;
    public AudioMixerSnapshot GameOver;
    public AudioMixerSnapshot SpeedUp;


	// Use this for initialization
	void Start () {
        GamePlayManager.OnGameStart += OnGameStart;
        GamePlayManager.OnGameOver += OnGameOver;
        GamePlayManager.OnBackToMainMenu += OnBackToMainMenu;

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


}
