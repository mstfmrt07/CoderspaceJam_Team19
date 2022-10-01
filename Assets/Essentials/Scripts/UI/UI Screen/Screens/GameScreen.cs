

public class GameScreen : UIScreen
{
    public ScoreUI scoreUI;

    public override void Load()
    {
        base.Load();
        scoreUI.IsActive = true;
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Close()
    {
        base.Close();
        scoreUI.IsActive = false;
    }
}
