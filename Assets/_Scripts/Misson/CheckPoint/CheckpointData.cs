using UnityEngine;
[CreateAssetMenu(fileName = "CheckpointData", menuName = "Scriptable Objects/CheckpointData")]
public class CheckpointData : ScriptableObject
{
    public string checkpointID;
    public CheckpointType checkpointType;
    public Vector3 position;
}
