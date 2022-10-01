using UnityEngine;

public class GameManager : MSingleton<GameManager>
{
    [Header("Values")]
    public float gameFlowAcceleration;
    public float maxGameSpeed;

    private bool isGamePlaying;
    private float gameFlowSpeed = 1f;
    public float GameFlowSpeed => gameFlowSpeed;

    public bool IsGamePlaying => isGamePlaying;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        if (isGamePlaying)
            return;

        InputManager.Instance.tapToStart.OnTap += StartGame;
        Player.Instance.hitbox.OnDestroy += FinishGame;

        GameEvents.OnGameLoad?.Invoke();
    }

    public void StartGame()
    {
        if (isGamePlaying)
            return;

        gameFlowSpeed = 1f;
        isGamePlaying = true;
        InputManager.Instance.tapToStart.OnTap -= StartGame;

        GameEvents.OnGameStarted?.Invoke();
    }

    private void Update()
    {
        if (!isGamePlaying)
            return;

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
