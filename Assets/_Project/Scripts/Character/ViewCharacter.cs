using UnityEngine;

public class ViewCharacter : MonoBehaviour
{
    private Animator _animator;
    private Character _character;
    private int _currentHealth;

    private int _moveKey = Animator.StringToHash("MovementSpeed");
    private int _healthKey = Animator.StringToHash("Health");
    private int _hitKey = Animator.StringToHash("Hit");
    private int _deadKey = Animator.StringToHash("Dead");
    private int _jumpKey = Animator.StringToHash("Jump");

    public void Initialize(Character character)
    {
        _character = character;
        _currentHealth = character.MaxHealth;
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        SetMovementParameter();
        SetHealthParameter();

        if (IsTakeDamage())
            TriggerDamageAnimation();

        if (IsAlive() == false)
            TriggerDeathAnimation();

        _animator.SetBool(_jumpKey, InJumpProcess());
    }

    private void SetMovementParameter() => _animator.SetFloat(_moveKey, (_character.CurrentDirectionToTarget).magnitude);

    private void SetHealthParameter() => _animator.SetFloat(_healthKey, _character.CurrentHealth / _character.MaxHealth);

    private void TriggerDamageAnimation() => _animator.SetTrigger(_hitKey);

    private void TriggerDeathAnimation() => _animator.SetBool(_deadKey, true);

    private bool InJumpProcess() => _character.InJumpProcess;

    private bool IsTakeDamage()
    {
        if (_character.CurrentHealth < _currentHealth)
        {
            _currentHealth = _character.CurrentHealth;
            return true;
        }

        return false;
    }

    private bool IsAlive() => _character.IsAlive;
}
