using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MSingleton<GameManager>
{
    private bool isGameStarted;

    public bool GameStarted => isGameStarted;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        if (isGameStarted)
            return;

        InputManager.Instance.tapToStart.OnTap += StartGame;
        Player.Instance.hitbox.OnDestroy += FinishGame;

        GameEvents.OnGameLoad?.Invoke();
    }

    public void StartGame()
    {
        if (isGameStarted)
            return;

        isGameStarted = true;
        InputManager.Instance.tapToStart.OnTap -= StartGame;

        GameEvents.OnGameStarted?.Invoke();
    }

    public void FinishGame()
    {
        if (!isGameStarted)
            return;

        isGameStarted = false;
        Player.Instance.hitbox.OnDestroy -= FinishGame;

        GameEvents.OnGameFailed?.Invoke();
    }

    public void RecoverGame()
    {
        //Todo implement Recover game
        if (isGameStarted)
            return;

        isGameStarted = true;
        GameEvents.OnGameRecovered?.Invoke();
    }

    public void RestartGame()
    {
        isGameStarted = false;

        RecycleBin.Instance.DisposeAll();
        LoadGame();
    }
}
