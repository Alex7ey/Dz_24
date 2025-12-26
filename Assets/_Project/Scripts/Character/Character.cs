using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IMovable, IRotate, IDamagable, IJumper, IHealable
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _maxHealth;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Material _material;

    private NavMeshAgent _agent;

    private Health _health;
    private NavMeshMover _navMeshMover;
    private AgentPathRotator _agentPathRotator;
    private NavMeshAgentJumper _agentJumper;

    public Vector3 CurrentPosition => transform.position;
    public Vector3 CurrentPositionTarget => _navMeshMover.CurrentPositionTarget;
    public Vector3 CurrentDirectionToTarget => CurrentPositionTarget - transform.position;

    public int MaxHealth => _maxHealth;
    public bool IsAlive => _health.IsAlive;
    public int CurrentHealth => _health.CurrentHealth;
    public bool InJumpProcess => _agentJumper.InProcess;

    public void MoveTo(RaycastHit[] hits)
    {
        if (_health.IsAlive == false || InJumpProcess)
            return;

        _navMeshMover.SetMovePoint(hits);
        SetLookAtPosition(CurrentPositionTarget);
    }

    public void SetLookAtPosition(Vector3 point)
    {
        _agentPathRotator.SetLookAtPosition(point);
    }

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        _agentJumper.Jump(offMeshLinkData, CurrentPositionTarget);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Heal(int health)
    {
        _health.AddHealth(health);
    }

    private void Awake()
    {
        _agent = GetComponentInChildren<NavMeshAgent>();
        _health = new(_maxHealth);

        _navMeshMover = new(_agent, _movementSpeed);
        _agentPathRotator = new(transform, _rotationSpeed, _agent);
        _agentJumper = new(_movementSpeed, _agent, this, _animationCurve);
    }

    private void Update()
    {
        _agentPathRotator.Update(Time.deltaTime);
    }
}
