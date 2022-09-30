using System.Collections;
using UnityEngine;

public class UIManager : MSingleton<UIManager>, IGameEventsHandler
{
    [Header("References")]
    public StartScreen startScreen;
    public GameScreen gameScreen;
    public WinScreen winScreen;
    public LoseScreen loseScreen;

    [Header("Values")]
    public float winScreenDelay;
    public float loseScreenDelay;

    private UIScreen currentScreen;

    private void Awake()
    {
        SubscribeGameEvents();
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnLevelLoad += OnLevelLoad;
        GameEvents.OnLevelStarted += OnLevelStarted;
        GameEvents.OnLevelCompleted += OnLevelCompleted;
        GameEvents.OnLevelFailed += OnLevelFailed;
    }

    public void SwitchScreen(UIScreen Screen)
    {
        if (currentScreen != null)
            CloseScreen(currentScreen);

        LoadScreen(Screen);
    }

    public void LoadScreen(UIScreen Screen)
    {
        Screen.Load();
        currentScreen = Screen;
    }

    public void ResetScreen(UIScreen Screen)
    {
        Screen.Reset();
    }

    public void CloseScreen(UIScreen Screen)
    {
        if (Screen == currentScreen)
            currentScreen = null;

        Screen.Close();
    }

    public void ResetAllScreens()
    {
        var screens = FindObjectsOfType<UIScreen>();

        foreach (var screen in screens)
            ResetScreen(screen);
    }

    public void CloseAllScreens()
    {
        var screens = FindObjectsOfType<UIScreen>();

        foreach (var screen in screens)
            CloseScreen(screen);
    }

    public IEnumerator SwitchScreenAfterSeconds(UIScreen screen, float duration)
    {
        yield return new WaitForSeconds(duration);
        SwitchScreen(screen);
    }

    public void OnLevelLoad()
    {
        ResetAllScreens();
        CloseAllScreens();

        SwitchScreen(startScreen);
    }

    public void OnLevelStarted()
    {
        SwitchScreen(gameScreen);
    }

    public void OnLevelCompleted()
    {
        StartCoroutine(SwitchScreenAfterSeconds(winScreen, winScreenDelay));
    }

    public void OnLevelFailed()
    {
        StartCoroutine(SwitchScreenAfterSeconds(loseScreen, loseScreenDelay));
    }
}