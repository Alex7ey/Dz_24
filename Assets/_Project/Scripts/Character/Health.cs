using UnityEngine;

public class Health
{
    private readonly int _maxHealth;

    private int _currentHealth;

    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public bool IsAlive => _currentHealth > 0;

    public int CurrentHealth => _currentHealth;

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || IsAlive == false)
            return;

        _currentHealth -= damage;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            return;
        }
    }

    public void AddHealth(int health)
    {
        if (IsAlive == false)
            return;

        _currentHealth += health;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
            return;
        }
    }
}
