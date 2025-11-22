using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : TMonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private Dictionary<GameObject, IObjectPool<GameObject>> poolDictionary;

    [Header("Global Pool Settings")]
    public bool collectionCheck = true;
    public int defaultCapacity = 10;
    public int maxPoolSize = 100;
    protected override void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject);

        poolDictionary = new Dictionary<GameObject, IObjectPool<GameObject>>();
    }
    public void Prewarm(GameObject prefab, int count)
    {
        if (!poolDictionary.ContainsKey(prefab))
            CreatePool(prefab);

        var pool = poolDictionary[prefab];

        for (int i = 0; i < count; i++)
        {
            var obj = pool.Get();
            pool.Release(obj);
        }
    }
    public GameObject Get(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
            CreatePool(prefab);

        return poolDictionary[prefab].Get();
    }
    public void Release(GameObject prefab, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning("Trying to release to a pool that does not exist.");
            Destroy(obj);
            return;
        }

        poolDictionary[prefab].Release(obj);
    }
    private void CreatePool(GameObject prefab)
    {
        IObjectPool<GameObject> newPool = new ObjectPool<GameObject>(
            () => CreateFunc(prefab),
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooled,
            collectionCheck,
            defaultCapacity,
            maxPoolSize
        );

        poolDictionary.Add(prefab, newPool);
    }

    private GameObject CreateFunc(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);

        // Gắn component để object tự biết nó thuộc prefab nào
        var pooledObj = obj.GetComponent<PooledObject>();
        if (pooledObj == null) pooledObj = obj.AddComponent<PooledObject>();

        pooledObj.prefab = prefab;

        return obj;
    }
    private void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);

        // // RESET những thứ hay bị dơ
        // var rb = obj.GetComponent<Rigidbody>();
        // if (rb)
        // {
        //     rb.velocity = Vector3.zero;
        //     rb.angularVelocity = Vector3.zero;
        // }
    }
    private void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    private void OnDestroyPooled(GameObject obj)
    {
        if (!Application.isPlaying)
            return; // Editor cleanup, bỏ qua tránh lỗi

        Destroy(obj);
    }

}
