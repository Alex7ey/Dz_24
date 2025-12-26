using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private Animator _animator;
    private Character _character;
    private DissolveEffect _dissolveEffect;

    private int _currentHealth;

    private readonly int _moveKey = Animator.StringToHash("MovementSpeed");
    private readonly int _healthKey = Animator.StringToHash("Health");
    private readonly int _hitKey = Animator.StringToHash("Hit");
    private readonly int _deadKey = Animator.StringToHash("Dead");
    private readonly int _jumpKey = Animator.StringToHash("Jump");

    private const float MovementThreshold = 0.2f;
    private const float _dissolveSpeed = 2f;

    public void PlayDissolveEffect() => _dissolveEffect.PlayDissolveEffect();

    private void Awake()
    {
        _dissolveEffect = new(GetComponentsInChildren<SkinnedMeshRenderer>(), this, _dissolveSpeed);
        _animator = GetComponent<Animator>();
        _character = GetComponentInParent<Character>();
    }

    private void Update()
    {
        SetMovementParameter();
        SetHealthParameter();
        SetJumpParameter();

        if (IsTakeDamage())
            TriggerDamageAnimation();

        if (IsAlive() == false)
            TriggerDeathAnimation();
    }

    private void SetMovementParameter()
    {
        float movementMagnitude = _character.CurrentDirectionToTarget.magnitude > MovementThreshold ? _character.CurrentDirectionToTarget.magnitude : 0;
        _animator.SetFloat(_moveKey, movementMagnitude);
    }

    private void SetHealthParameter() => _animator.SetFloat(_healthKey, _character.CurrentHealth / _character.MaxHealth);

    private void SetJumpParameter() => _animator.SetBool(_jumpKey, _character.InJumpProcess);

    private void TriggerDamageAnimation() => _animator.SetTrigger(_hitKey);

    private void TriggerDeathAnimation()
    {
        _animator.SetBool(_deadKey, true);
    }

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
