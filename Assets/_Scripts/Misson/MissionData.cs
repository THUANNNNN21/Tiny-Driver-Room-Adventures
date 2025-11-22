using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "Scriptable Objects/MissionData")]
public class MissionData : ScriptableObject
{
    public string missionName;

    public MissionType missionType;

    public List<CheckpointData> listCheckpoints;

    public int rewardCoins;
    public int rewardScores;
    public int rewardEnergy;

    public float timeLimit;

    // public List<BonusGoal> bonusGoals;
}

