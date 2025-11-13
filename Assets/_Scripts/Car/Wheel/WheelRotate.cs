using UnityEngine;

public class WheelRotate : TMonoBehaviour
{
    [SerializeField] private CarCtlr carCtlr;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtlr();
    }
    private void LoadCarCtlr()
    {
        if (this.carCtlr != null) return;
        this.carCtlr = this.GetComponentInParent<CarCtlr>();
        Debug.LogWarning($"<color=green>Load CarCtlr:</color> {this.carCtlr.gameObject.name}", this.carCtlr);
    }
    private void FixedUpdate()
    {
        this.RotateWheel();
    }
    protected void RotateWheel()
    {
        if (carCtlr == null || carCtlr.CarMovement == null) return;
        float speed = carCtlr.CarMovement.Speed;
        float rotationAngle = speed * Time.fixedDeltaTime; // Assuming wheel radius is 0.3 units
        transform.Rotate(Vector3.right, rotationAngle);
    }
}
