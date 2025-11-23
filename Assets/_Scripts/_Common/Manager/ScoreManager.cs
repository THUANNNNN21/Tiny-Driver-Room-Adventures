using System;
using UnityEngine;

public class ScoreManager : TMonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    [SerializeField] protected int currentScore;
    public int HighScore { get; private set; }
    public event Action<int> OnScoreChanged;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHighScore();
    }
    public void AddScore(int amount)
    {
        this.currentScore += amount;
        Debug.Log($"Added {amount} score. New total: {this.currentScore}");
        OnScoreChanged?.Invoke(this.currentScore);

        if (this.currentScore > this.HighScore)
        {
            this.HighScore = this.currentScore;
            this.SaveHighScore();
        }
    }

    public void ResetScore()
    {
        this.currentScore = 0;
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", this.HighScore);
        PlayerPrefs.Save();
    }

    void LoadHighScore()
    {
        this.HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
