using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUI : Activatable
{
    [Header("References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    [Header("Constants")]
    public string scorePrefix;
    public string highscorePrefix;

    protected override void OnActivate()
    {
        base.OnActivate();

        GameManager.Instance.OnHighScoreBeaten += OnHighScoreBeaten;
        highscoreText.text = highscorePrefix + GameManager.Instance.HighScore.ToString("000");
    }

    protected override void Tick()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var score = GameManager.Instance.Score;
        var high = GameManager.Instance.HighScore;

        scoreText.text = scorePrefix + score.ToString("000");
        if (score > high)
            highscoreText.text = highscorePrefix + score.ToString("000");
    }

    private void OnHighScoreBeaten()
    {
        GameManager.Instance.OnHighScoreBeaten -= OnHighScoreBeaten;

        DOTween.Kill("HighScore");
        var originalColor = highscoreText.color;
        highscoreText.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f, 20).SetId("HighScore");
        highscoreText.DOColor(Color.red, 0.6f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetId("HighScore").OnKill(() => highscoreText.color = originalColor);
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();

        DOTween.Kill("HighScore");
        GameManager.Instance.OnHighScoreBeaten -= OnHighScoreBeaten;
    }

}
