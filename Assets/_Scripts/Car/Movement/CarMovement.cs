using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : TMonoBehaviour
{
    [SerializeField] protected CarCtlr carCtlr;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float deceleration = 1f;
    [SerializeField] protected float speed = 0f;
    public float Speed => speed;
    [SerializeField] protected float rotateSpeed = 1f;
    [SerializeField] protected float maxSpeed = 150f;
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected float energyConsumptionRate = 0.01f; // Energy consumed per unit of speed
    public bool IsMoving => isMoving;
    protected InputAction moveAction;
    protected Vector2 moveInput;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarController();
        this.LoadMoveAction();
    }
    protected virtual void LoadCarController()
    {
        if (this.carCtlr != null) return;
        this.carCtlr = GetComponentInParent<CarCtlr>();
        Debug.LogWarning($"CarMovement: LoadCarController in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadMoveAction()
    {
        moveAction = Resources.Load<InputActionAsset>("InputSystem_Actions").FindActionMap("Player").FindAction("Move");
    }
    protected void OnEnable()
    {
        if (moveAction == null)
        {
            this.LoadMoveAction();
        }
        moveAction.Enable();
    }
    public void UpdateMove()
    {
        this.Move();
        this.CheckIsMoving();
        this.MoveConsumeEnergy();
    }
    protected void Move()
    {
        this.moveInput = moveAction.ReadValue<Vector2>();
        this.CalculateSpeed();
        this.ApplyFriction();
        Vector3 rotation = this.CalculateRotation();
        this.carCtlr.CarRigidbody.linearVelocity = this.speed * Time.fixedDeltaTime * transform.forward;
        if (this.isMoving)
        {
            this.carCtlr.CarRigidbody.MoveRotation(this.carCtlr.CarRigidbody.rotation * Quaternion.Euler(rotation));
        }
    }
    protected void CalculateSpeed()
    {
        float verticalInput = this.moveInput.y;
        if (verticalInput > 0) // Gas pedal
        {
            this.speed += this.acceleration * Time.fixedDeltaTime;
            this.speed = Mathf.Min(this.speed, this.maxSpeed);
        }
        else if (verticalInput < 0) // Brake/Reverse
        {
            if (this.speed > 0)
            {
                this.speed -= this.acceleration * 3f * Time.fixedDeltaTime; // Stronger deceleration when braking
                this.speed = Mathf.Max(this.speed, 0);
            }
            else
            {
                this.speed -= this.acceleration * Time.fixedDeltaTime;
                this.speed = Mathf.Max(this.speed, -this.maxSpeed * 0.5f); // Reverse speed limit
            }
        }

    }
    protected void ApplyFriction()
    {
        if (this.speed > 0)
        {
            this.speed -= this.deceleration * Time.fixedDeltaTime; // Natural deceleration 
            this.speed = Mathf.Max(this.speed, 0);
        }
        else if (this.speed < 0)
        {
            this.speed += this.deceleration * Time.fixedDeltaTime; // Natural deceleration 
            this.speed = Mathf.Min(this.speed, 0);
        }
    }
    protected Vector3 CalculateRotation()
    {
        float horizontalInput = this.moveInput.x;
        float rotationAngle = horizontalInput * this.rotateSpeed; // Adjust the multiplier for sensitivity

        return new Vector3(0f, rotationAngle, 0f);
    }
    public bool CheckIsMoving()
    {
        this.isMoving = Mathf.Abs(this.speed) < -1 || Mathf.Abs(this.speed) > 1;
        return this.isMoving;
    }
    protected void MoveConsumeEnergy()
    {
        if (this.carCtlr.CarEnergy == null) return;
        if (!this.isMoving) return;
        if (this.speed > 150f)
        {
            this.energyConsumptionRate = 0.02f; // Increase consumption rate at high speed
        }
        else if (this.speed < 20f)
        {
            this.energyConsumptionRate = 0.02f; // Decrease consumption rate at low speed
        }
        else
        {
            this.energyConsumptionRate = 0.01f; // Normal consumption rate
        }
        float energyToConsume = this.energyConsumptionRate * Mathf.Abs(this.speed) * Time.fixedDeltaTime;
        this.carCtlr.CarEnergy.ConsumeEnergy(energyToConsume);
    }
    protected void CanMove()
    {

    }
    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }
}
