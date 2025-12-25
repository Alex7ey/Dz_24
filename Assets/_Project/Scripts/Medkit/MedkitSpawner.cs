using System.Collections;
using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float _radius = 10;
    [SerializeField] private Medkit _medKitPrefab;

    private bool _isActive;
    private Coroutine _coroutine;
    private Transform _targetPosition;

    public void Initialize(Transform transform) => _targetPosition = transform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _isActive = !_isActive;

        if (_isActive && _coroutine == null)
            _coroutine = StartCoroutine(RunSpawningProcess());
    }

    private IEnumerator RunSpawningProcess()
    {
        while (_isActive)
        {
            Spawn();
            yield return new WaitForSeconds(_spawnCooldown);
        }

        _coroutine = null;
        yield break;
    }

    private void Spawn()
    {
        Vector3 spawnPosition = _targetPosition.position + GetSpawnPosition();
        Instantiate(_medKitPrefab, spawnPosition, Quaternion.identity, transform);
    }

    private Vector3 GetSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = _radius * Mathf.Cos(angle);
        float y = _radius * Mathf.Sin(angle);

        return new Vector3(x, 0, y);
    }
}
