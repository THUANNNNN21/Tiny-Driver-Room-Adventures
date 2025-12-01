using UnityEngine;
using System.Collections.Generic;

public abstract class Subject : TMonoBehaviour
{
    [Header("Observers")]
    [SerializeField] private List<IObserver> observers = new();
    protected override void Awake()
    {
        base.Awake();
        this.Subcribe();
    }
    private void OnDestroy()
    {
        this.Unsubcribe();
    }
    protected abstract void Subcribe();
    protected abstract void Unsubcribe();
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSubject();
    }
    protected abstract void LoadSubject();
    public void AddObserver(IObserver observer)
    {
        this.observers.Add(observer);
        Debug.Log("Add observer: " + observer);
    }
    protected void NoticeObserver()
    {
        Debug.Log("Subject: NoticeObserver to " + observers.Count + " observers.");
        foreach (IObserver observer in this.observers)
        {
            observer.OnSujectNotice();
        }
    }
}