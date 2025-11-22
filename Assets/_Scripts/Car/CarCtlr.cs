using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CarStateMachine))]
public class CarCtlr : TMonoBehaviour
{
    private static CarCtlr instance;
    public static CarCtlr Instance => instance;

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
    [SerializeField] CarStateMachine carStateMachine;
    public CarStateMachine CarStateMachine { get => carStateMachine; }
    [SerializeField] Rigidbody carRigidbody;
    public Rigidbody CarRigidbody { get => carRigidbody; }
    [SerializeField] CarEnergy carEnergy;
    public CarEnergy CarEnergy { get => carEnergy; }
    [SerializeField] CarMovement carMovement;
    public CarMovement CarMovement { get => carMovement; }
    [SerializeField] CarImpact carImpact;
    public CarImpact CarImpact { get => carImpact; }
    [SerializeField] MissionCtrl missionCtrl;
    public MissionCtrl MissionCtrl { get => missionCtrl; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarStateMachine();
        this.LoadRigidbody();
        this.LoadCarEnergy();
        this.LoadCarMovement();
        this.LoadCarImpact();
        this.LoadMissionCtrl();
    }
    protected virtual void LoadCarStateMachine()
    {
        if (this.carStateMachine != null) return;
        this.carStateMachine = GetComponent<CarStateMachine>();
        Debug.LogWarning($"CarCtlr: LoadCarStateMachine in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadRigidbody()
    {
        if (this.carRigidbody != null) return;
        this.carRigidbody = GetComponent<Rigidbody>();
        Debug.LogWarning($"CarCtlr: LoadRigidbody in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadCarEnergy()
    {
        if (this.carEnergy != null) return;
        this.carEnergy = GetComponentInChildren<CarEnergy>();
        Debug.LogWarning($"CarCtlr: LoadCarEnergy in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadCarMovement()
    {
        if (this.carMovement != null) return;
        this.carMovement = GetComponentInChildren<CarMovement>();
        Debug.LogWarning($"CarCtlr: LoadCarMovement in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadCarImpact()
    {
        if (this.carImpact != null) return;
        this.carImpact = GetComponentInChildren<CarImpact>();
        Debug.LogWarning($"CarCtlr: LoadCarImpact in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadMissionCtrl()
    {
        if (this.missionCtrl != null) return;
        this.missionCtrl = GameObject.Find("Missons").GetComponent<MissionCtrl>();
        Debug.LogWarning($"CarCtlr: LoadMissionCtrl in {gameObject.name} ", gameObject);
    }
    protected void FixedUpdate()
    {
        this.carMovement.UpdateMove();
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) return;
        bool shouldCrash = this.carImpact.ApplyImpactForce(collision);
        if (shouldCrash && carStateMachine != null)
        {
            this.carStateMachine.ChangeState(carStateMachine.CarCrashState);
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) return;
        this.carImpact.EnterCheckPoint(other);
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) return;
        this.carImpact.ExitCheckPoint(other);
    }
}
