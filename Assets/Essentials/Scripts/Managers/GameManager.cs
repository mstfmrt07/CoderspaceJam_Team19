using System;
using UnityEngine;

public class GameManager : MSingleton<GameManager>
{
    [Header("Values")]
    public float gameFlowAcceleration;
    public float maxGameSpeed;

    private bool isGamePlaying;
    private float gameFlowSpeed = 1f;
    private int score;
    private bool highScoreBeaten;

    public float GameFlowSpeed => gameFlowSpeed;
    public bool IsGamePlaying => isGamePlaying;
    public int Score => score;
    public int HighScore => SaveManager.Instance.HighScore;

    public Action OnHighScoreBeaten;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        if (isGamePlaying)
            return;

        Player.Instance.hitbox.OnDestroy += FinishGame;

        score = 0;
        highScoreBeaten = false;

        GameEvents.OnGameLoad?.Invoke();
    }

    public void StartGame()
    {
        if (isGamePlaying)
            return;

        gameFlowSpeed = 1f;
        isGamePlaying = true;

        GameEvents.OnGameStarted?.Invoke();
    }

    private void Update()
    {
        if (!isGamePlaying)
            return;

        score = (int)Player.Instance.distanceMeter.Distance;

        if (score > HighScore && HighScore > 0 && !highScoreBeaten)
        {
            highScoreBeaten = true;
            OnHighScoreBeaten?.Invoke();
        }


        if (gameFlowSpeed < maxGameSpeed)
            gameFlowSpeed += gameFlowAcceleration * Time.deltaTime;
    }

    public void FinishGame()
    {
        if (!isGamePlaying)
            return;

        isGamePlaying = false;
        Player.Instance.hitbox.OnDestroy -= FinishGame;

        GameEvents.OnGameFailed?.Invoke();
    }

    public void RecoverGame()
    {
        //Todo implement Recover game
        if (isGamePlaying)
            return;

        isGamePlaying = true;
        GameEvents.OnGameRecovered?.Invoke();
    }

    public void RestartGame()
    {
        isGamePlaying = false;
        gameFlowSpeed = 1f;

        RecycleBin.Instance.DisposeAll();
        LoadGame();
    }
}
