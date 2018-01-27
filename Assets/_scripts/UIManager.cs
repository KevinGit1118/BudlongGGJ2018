using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject MainMenuPanel = null;
    public GameObject GameOverPanel = null;
    public GameObject InGamePanel = null;
    public Text TimerText = null;

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
        //updtate timer
        if (GamePlayManager.CurGameState == GamePlayManager.GameState.InGame)
        {
            TimerText.text = GamePlayManager.Instance.GetRestTime().ToString();
        }
	}

    void OnGameStart()
    {
        //when game start, close main menu
        MainMenuPanel.SetActive(false);
        InGamePanel.SetActive(true);
    }
    void OnGameOver()
    {
        GameOverPanel.SetActive(true);
        InGamePanel.SetActive(false);
    }

    void OnBackToMainMenu()
    {
        GameOverPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        InGamePanel.SetActive(false);
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
