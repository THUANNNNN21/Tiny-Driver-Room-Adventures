using UnityEngine;

public class UIScoreText : BaseText
{
    private void OnEnable()
    {
        ScoreManager.Instance.OnScoreChanged += HandleScoreChanged;
    }
    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= HandleScoreChanged;
    }
    private void HandleScoreChanged(int newScore)
    {
        uiText.text = "Score: " + newScore.ToString();
    }
}
