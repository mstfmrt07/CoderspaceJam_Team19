using UnityEngine;

public class InputManager : MSingleton<InputManager>, IGameEventsHandler
{
    public TapDetector jumpButton;
    public TapDetector dashButton;

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
    }

    public void OnGameStarted()
    {
        jumpButton.OnTap += Player.Instance.AttemptJump;
        dashButton.OnTap += Player.Instance.AttemptDash;

        jumpButton.IsActive = true;
        dashButton.IsActive = true;
    }

    public void OnGameFailed()
    {
        jumpButton.OnTap -= Player.Instance.AttemptJump;
        dashButton.OnTap -= Player.Instance.AttemptDash;

        jumpButton.IsActive = true;
        dashButton.IsActive = true;
    }

    public void OnGameRecovered()
    {
    }
}
