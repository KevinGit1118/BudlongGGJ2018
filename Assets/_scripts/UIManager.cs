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
    public Image TimerBar = null;

    public GameObject LeaderboardEntryRef = null;
    private List<GameObject> LeaderboardEntries = new List<GameObject>();
    private bool askPlayerData = true;

    public InputField userName = null;
    public Text userScroe = null;


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
            TimerBar.fillAmount = GamePlayManager.Instance.GetRestTime() / GamePlayManager.Instance.timeTable[GamePlayManager.CurStage];
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
        PlayersData.Instance.UpdateLeaderBoard(userName.text, GamePlayManager.Instance.centerPoint.currentPoint);
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

    public void BtnResetPlayerData()
    {
        // PlayerPrefs.DeleteAll();
        for (int i = 0; i < 10; ++i)
        {
            PlayerPrefs.SetString("name" + i, "");
            PlayerPrefs.SetInt("score" + i,0);
            PlayersData.LeaderLine temp = new PlayersData.LeaderLine();
            temp.name = "";
            temp.score = 0;
            PlayersData.Instance.leaderBoard[i] = temp;
        }
        UpdateLeaderBoard();

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
            if (string.IsNullOrEmpty(lName) && lScore == 0)
            {
                LeaderboardEntries[i].GetComponentInChildren<Text>().text = "";
                continue;
            }
            LeaderboardEntries[i].GetComponentInChildren<Text>().text = lName + " : " + lScore;

        }

    }
}
