using UnityEngine;

public class MedKitCollector : MonoBehaviour
{
    private IHealable _ihealable;

    private void Awake()
    {
        _ihealable = GetComponent<IHealable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealer healer))
        {
            healer.Heal(_ihealable);
        }
    }
}
