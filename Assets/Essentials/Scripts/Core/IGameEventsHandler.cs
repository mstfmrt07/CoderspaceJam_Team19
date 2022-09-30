public interface IGameEventsHandler
{
    void SubscribeGameEvents();
    void OnLevelLoad();
    void OnLevelStarted();
    void OnLevelCompleted();
    void OnLevelFailed();
}