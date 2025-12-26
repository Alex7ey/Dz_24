using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    private Character _character;

    private float _maxValue;
    private int _currentHealth;
    private const float MinValue = 0;

    public void Initialize(Character character)
    {
        _character = character;
        _maxValue = character.MaxHealth;
    }

    private void Update()
    {
        if(_currentHealth != _character.CurrentHealth)
        {
            SetValueProgress(_character.CurrentHealth);

            _currentHealth = _character.CurrentHealth;
        }
    }

    private void SetValueProgress(float currentValue)
    {
        if(_progressBar.type != Image.Type.Filled)
        {
            Debug.LogWarning("Image is not Filled type!");
            return;
        }

        float fillAmount = Mathf.InverseLerp(MinValue, _maxValue, currentValue);
        _progressBar.fillAmount = fillAmount;
    }
}
