using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject MainMenuPanel = null;
    public GameObject GameOverPanel = null;


    void Awake()
    {
        GamePlayManager.OnGameStart += OnGameStart;
        GamePlayManager.OnGameOver += OnGameOver;
        GamePlayManager.OnBackToMainMenu += OnBackToMainMenu;
    }

    void OnDestroy()
    {
        GamePlayManager.OnGameStart -= OnGameStart;
        GamePlayManager.OnGameOver -= OnGameOver;
        GamePlayManager.OnBackToMainMenu -= OnBackToMainMenu;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGameStart()
    {
        //when game start, close main menu
        MainMenuPanel.SetActive(false);
    }
    void OnGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    void OnBackToMainMenu()
    {
        GameOverPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void BtnStart()
    {
        GamePlayManager.OnGameStart();
    }

    public void BtnToMainMenu()
    {
        GamePlayManager.OnBackToMainMenu();
    }
}
