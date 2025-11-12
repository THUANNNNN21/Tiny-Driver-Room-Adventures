using UnityEngine;

public abstract class TMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.Reset();
    }
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.LoadValues();
    }
    protected virtual void LoadComponents()
    {
        // Load necessary components here
    }
    protected virtual void LoadValues()
    {
        // Load necessary values here
    }
}
