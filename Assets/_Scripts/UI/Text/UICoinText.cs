using UnityEngine;

public class UICoinText : BaseText
{
    private void OnEnable()
    {
        CoinManager.Instance.OnCoinChanged += HandleCoinChanged;
        HandleCoinChanged(CoinManager.Instance.CurrentCoins);
    }
    private void OnDisable()
    {
        CoinManager.Instance.OnCoinChanged -= HandleCoinChanged;
    }
    private void HandleCoinChanged(int newCoins)
    {
        uiText.text = "Coins: " + newCoins.ToString();
    }
}