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
                _instance = new GamePlayManager();
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
    }

    void StartGame()
    {
        curGameState = GameState.InGame;     
    }

    void EndGame()
    {
        curGameState = GameState.GameOver;
    }

    void BackToMainMenu()
    {
        curGameState = GameState.MainMenu;
    }


}
