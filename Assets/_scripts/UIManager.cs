using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject MainMenuPanel = null;
    public GameObject GameOverPanel = null;
    public GameObject InGamePanel = null;
    public GameObject LeaderBoardPanel = null;
    public Text TimerText = null;

    public GameObject LeaderboardEntryRef = null;
    private List<GameObject> LeaderboardEntries = new List<GameObject>();
    private bool askPlayerData = true;

    public InputField userName = null;
    public Text userScroe = null;
    public CenterPoint centerPoint = null;


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
        GenerateLeaderBoader();
    }
	
	// Update is called once per frame
	void Update () {

        if (askPlayerData && PlayersData.Instance.hasInit )
        {
            UpdateLeaderBoard();
            askPlayerData = false;
        }

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
        //update leaderboard player data here
        PlayersData.Instance.UpdateLeaderBoard(userName.text, centerPoint.currentPoint);
        UpdateLeaderBoard();
    }

    public void BtnStart()
    {
        GamePlayManager.OnGameStart();
    }

    public void BtnToMainMenu()
    {
        GamePlayManager.OnBackToMainMenu();
    }


    public void GenerateLeaderBoader()
    {
        //generate 10 entry
        for (int i = 0; i < 10; i++)
        {
            GameObject newEntry = Instantiate(LeaderboardEntryRef, LeaderBoardPanel.transform);
            LeaderboardEntries.Add(newEntry);
        }

    }
    public void UpdateLeaderBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            string lName =  PlayersData.Instance.leaderBoard[i].name;
            int lScore = PlayersData.Instance.leaderBoard[i].score;
            LeaderboardEntries[i].GetComponentInChildren<Text>().text = lName + " : " + lScore;
        }

    }
}
