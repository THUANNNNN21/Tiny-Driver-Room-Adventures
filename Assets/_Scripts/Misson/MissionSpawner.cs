using System.Collections.Generic;
using UnityEngine;

public class MissionSpawner : TMonoBehaviour
{
    [SerializeField] protected MissionCtrl missionCtrl;
    [SerializeField] protected GameObject holderCheckPoints;
    [SerializeField] protected List<GameObject> checkPoints = new();
    public List<GameObject> CheckPoints => checkPoints;
    [SerializeField] private CarImpact carImpact;
    protected override void Awake()
    {
        base.Awake();
        carImpact.OnCompleteCheckpoint += SpawnNextCheckpoint;
    }
    private void OnDestroy()
    {
        carImpact.OnCompleteCheckpoint -= SpawnNextCheckpoint;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionCtrl();
        this.LoadSubjectToObserve();
        this.LoadHolderCheckPoints();
        this.LoadCheckPoints();
    }
    private void LoadMissionCtrl()
    {
        if (this.missionCtrl != null) return;
        this.missionCtrl = GetComponentInParent<MissionCtrl>();
        Debug.LogWarning($"MissionSpawner: LoadMissionCtrl in {gameObject.name} ", gameObject);
    }
    private void LoadSubjectToObserve()
    {
        this.LoadCarImpact();
    }
    protected virtual void LoadCarImpact()
    {
        if (this.carImpact != null) return;
        this.carImpact = FindFirstObjectByType<CarImpact>();
        Debug.LogWarning($"MissionSpawner: LoadCarImpact in {gameObject.name} ", gameObject);
    }
    private void LoadHolderCheckPoints()
    {
        if (this.holderCheckPoints != null) return;
        this.holderCheckPoints = transform.parent.Find("CheckPoints").gameObject;
        Debug.LogWarning($"MissionSpawner: LoadHolderCheckPoints in {gameObject.name} ", gameObject);
    }
    private void LoadCheckPoints()
    {
        if (this.checkPoints != null && this.checkPoints.Count > 0) return;
        this.checkPoints = new List<GameObject>(Resources.LoadAll<GameObject>("MissionPrefab"));
    }
    void Start()
    {
        GameObject obj = PoolManager.Instance.Get(this.GetRandomCheckPoint());
        obj.transform.SetParent(this.holderCheckPoints.transform);
    }
    private void SpawnNextCheckpoint()
    {
        GameObject obj = PoolManager.Instance.Get(this.GetRandomCheckPoint());
        obj.transform.SetParent(this.holderCheckPoints.transform);
        obj.transform.position = this.GetRandomPosition();
    }
    protected GameObject GetRandomCheckPoint()
    {
        int index = Random.Range(0, this.checkPoints.Count);
        GameObject randomCheckPoint = this.checkPoints[index];
        Debug.Log("Random CheckPoint: " + randomCheckPoint.name);
        return randomCheckPoint;
    }
    protected Vector3 GetRandomPosition()
    {
        float x = Random.Range(-100f, 100f);
        float y = 0f;
        float z = Random.Range(-100f, 100f);
        return new Vector3(x, y, z);
    }
}
