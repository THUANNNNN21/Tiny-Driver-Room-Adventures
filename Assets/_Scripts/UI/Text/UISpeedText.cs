using UnityEngine;

public class UISpeedText : BaseText
{
    private void FixedUpdate()
    {
        this.UpdateSpeedText();
    }
    private void UpdateSpeedText()
    {
        float speed = CarCtlr.Instance.CarMovement.Speed;
        uiText.text = Mathf.RoundToInt(speed).ToString() + " M/H";
    }
}
