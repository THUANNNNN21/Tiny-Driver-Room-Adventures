using System;
using UnityEngine;

public class CoinManager : TMonoBehaviour
{
    private static CoinManager instance;
    public static CoinManager Instance => instance;

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
    [SerializeField] protected int currentCoins;
    public int CurrentCoins => currentCoins;
    public event Action<int> OnCoinChanged;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentCoins();
    }
    public void AddCoins(int amount)
    {
        this.currentCoins += amount;
        Debug.Log($"Added {amount} coins. New total: {this.currentCoins}");
        this.SaveCurrentCoins();
        OnCoinChanged?.Invoke(this.currentCoins);
    }
    void SaveCurrentCoins()
    {
        PlayerPrefs.SetInt("CurrentCoins", currentCoins);
        PlayerPrefs.Save();
    }
    void LoadCurrentCoins()
    {
        this.currentCoins = PlayerPrefs.GetInt("CurrentCoins");
    }
}
