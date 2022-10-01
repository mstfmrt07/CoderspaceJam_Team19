using UnityEngine;

public class InputManager : MSingleton<InputManager>, IGameEventsHandler
{
    public TapDetector tapToStart;

    private void Awake()
    {
        SubscribeGameEvents();
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnGameLoad += OnGameLoad;
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameFailed += OnGameFailed;
        GameEvents.OnGameRecovered += OnGameRecovered;
    }

    public void OnGameLoad()
    {
        tapToStart.IsActive = true;
    }

    public void OnGameStarted()
    {
        tapToStart.IsActive = false;
    }

    public void OnGameFailed()
    {
    }

    public void OnGameRecovered()
    {
        throw new System.NotImplementedException();
    }
}
