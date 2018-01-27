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

    public static GameState curGameState = GameState.MainMenu;



    void Awake()
    {
        OnGameStart += StartGame;
        OnGameOver += EndGame;
    }
    void OnDestroy()
    {
        OnGameStart -= StartGame;
        OnGameOver -= EndGame;
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


}
