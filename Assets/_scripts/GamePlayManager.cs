using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GamePlayManager : MonoBehaviour {

    public enum GameState
    {
        MainMenu,
        InGame,
        GameOver
    }

    static public GamePlayManager _instance;

    static public GamePlayManager Instance
    {
        get {

            if (_instance == null)
            {
                _instance = FindObjectOfType<GamePlayManager>();
            }

            return _instance;
        }
    }

    public static Action OnGameStart = delegate { };
    public static Action OnGameOver = delegate { };
    public static Action OnBackToMainMenu = delegate { };

    private static GameState curGameState = GameState.MainMenu;

    public static GameState CurGameState
    {
        get { return curGameState; }
    }


    public float timer = 0;
    public List<float> timeTable = new List<float>();
    private static int curStage = 0;
    public static int CurStage { get { return curStage; } }

    public Player[] players = new Player[3];
    public CenterPoint centerPoint;
    private bool EnableTimer = false;
    void Awake()
    {
        OnGameStart += StartGame;
        OnGameOver += EndGame;
        OnBackToMainMenu += BackToMainMenu;
    }
    void OnDestroy()
    {
        OnGameStart -= StartGame;
        OnGameOver -= EndGame;
        OnBackToMainMenu -= BackToMainMenu;
    }

    void Start()
    {
        curGameState = GameState.MainMenu;
        EnableTimer = false;

    }

    void StartGame()
    {
        curGameState = GameState.InGame;
        EnableTimer = true;
        timer = 0;
    }

    void EndGame()
    {
        curGameState = GameState.GameOver;
        EnableTimer = false;
        timer = 0;
    }

    void BackToMainMenu()
    {
        curGameState = GameState.MainMenu;
        EnableTimer = false;

    }

    void Update()
    {
        if (curGameState == GameState.InGame && EnableTimer)
        {
            timer += Time.deltaTime;
            //time
            if (timer > timeTable[curStage])
            {
                EnableTimer = false;
                foreach (Player p in players)
                {
                    if(p.isOn)
                    {
                        p.FireColorBall();
                        centerPoint.AddEstimateColorBallNum();
                    }
                }
                timer = 0;
            }
        }
    }

    public void ResetTimer()
    {
        EnableTimer = true;
        timer = 0;
    }

    public void StopTimer()
    {
        EnableTimer = false;
    }

    public float GetRestTime()
    {
        if(timeTable.Count == 0)
            return 0;
        return Mathf.Max(timeTable[curStage] - timer, 0);
    }


}
