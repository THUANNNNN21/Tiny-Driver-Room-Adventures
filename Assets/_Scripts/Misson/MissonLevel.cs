using System.Collections.Generic;
using UnityEngine;

public class MissionLevel : TMonoBehaviour
{
    [SerializeField] protected int level = 1;
    [SerializeField] private float timeDecreaseRate = 0.9f;
    // Growth multipliers per level (1.0 = no growth, 1.2 = +20% per level)
    [SerializeField] private float scoreEncreaseRate = 1.2f;
    [SerializeField] private float coinEncreaseRate = 1.15f;
    public int Level => level;
    public void SetLevel(int level)
    {
        this.level = level;
    }

    public float GetTimeForLevel(float time)
    {
        float newTimeLimit = (time * Mathf.Pow(this.timeDecreaseRate, this.level - 1));
        return newTimeLimit;
    }
    public int GetScoreForLevel(int baseScore)
    {
        // If level is 1 or invalid, return base
        if (this.level <= 1) return Mathf.Max(0, baseScore);

        // Use exponential growth with configurable multiplier per level
        // Example: scoreEncreaseRate = 1.2 means +20% per level
        float multiplier = Mathf.Pow(this.scoreEncreaseRate, this.level - 1);
        float newScore = baseScore * multiplier;

        // Safety: clamp to non-negative and to int range
        newScore = Mathf.Clamp(newScore, 0f, (float)int.MaxValue);
        return Mathf.RoundToInt(newScore);
    }

    public int GetCoinForLevel(int baseCoin)
    {
        if (this.level <= 1) return Mathf.Max(0, baseCoin);

        // Coins often grow slower than score; use a separate multiplier
        float multiplier = Mathf.Pow(this.coinEncreaseRate, this.level - 1);
        float newCoin = baseCoin * multiplier;

        newCoin = Mathf.Clamp(newCoin, 0f, (float)int.MaxValue);
        return Mathf.RoundToInt(newCoin);
    }
    // public void SetCurrentMission(MissionData missionData)
    // {
    //     this.currentMission = missionData;
    // }
}