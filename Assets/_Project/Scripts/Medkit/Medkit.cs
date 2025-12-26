using UnityEngine;

public class Medkit : MonoBehaviour, IHealer
{
    [SerializeField] private int _healAmount;

    public void Heal(IHealable healable)
    {
        healable.Heal(_healAmount);
        Destroy(gameObject);
    }
}
