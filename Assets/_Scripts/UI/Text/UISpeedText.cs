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
        uiText.text = Mathf.RoundToInt(Mathf.Abs(speed)).ToString() + " M/H";
    }
}
