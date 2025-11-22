using UnityEngine;

// Component cho mỗi object trong pool
public class PooledObject : TMonoBehaviour
{
    public GameObject prefab;   // Prefab gốc của object này

    private bool isReleased = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefab();
    }
    private void LoadPrefab()
    {
        if (this.prefab != null) return;
        this.prefab = this.gameObject;
    }

    private void OnEnable()
    {
        isReleased = false;
    }

    // Hàm public để tự trả về pool
    public void Release()
    {
        if (isReleased) return; // tránh release 2 lần
        isReleased = true;

        PoolManager.Instance.Release(prefab, gameObject);
    }
}
