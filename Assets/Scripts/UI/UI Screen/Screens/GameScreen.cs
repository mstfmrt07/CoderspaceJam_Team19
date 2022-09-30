using UnityEngine.UI;

public class GameScreen : UIScreen
{
    public Text levelText;

    public override void Load()
    {
        base.Load();

        levelText.text = "LEVEL " + LevelManager.Instance.CurrentLevelNumber;
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Close()
    {
        base.Close();
    }
}
