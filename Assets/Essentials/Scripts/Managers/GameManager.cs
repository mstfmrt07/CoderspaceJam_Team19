using System;
using UnityEngine;

public class GameManager : MSingleton<GameManager>, IResettable
{
    private bool isGameStarted = false;

    public bool GameStarted => isGameStarted;

    private void Awake()
    {
        ApplyReset();
    }

    public void StartGame()
    {
        if (isGameStarted)
            return;

        isGameStarted = true;
        GameEvents.OnLevelStarted?.Invoke();
    }

    public void FinishGame(bool winCondition)
    {
        if (!isGameStarted)
            return;

        if (winCondition)
        {
            GameEvents.OnLevelCompleted?.Invoke();
        }
        else
        {
            GameEvents.OnLevelFailed?.Invoke();
        }

        isGameStarted = false;
    }

    public void ApplyReset()
    {
        isGameStarted = false;
    }
}
