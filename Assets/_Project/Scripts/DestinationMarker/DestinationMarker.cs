using UnityEngine;

public class DestinationMarker : MonoBehaviour
{
    [SerializeField] private Transform _marker;
    [SerializeField] private float _collectionDistance = 0.5f;

    private IMovable _movable;

    private Vector3 _targetPosition;

    public void Initialize(IMovable movable)
    {
        _movable = movable;

        _targetPosition = _movable.CurrentPosition;
        DisableMarker();
    }

    public void Update()
    {

        if (_targetPosition != _movable.CurrentPositionTarget)
        {
            SetMarkerPosition();
            EnableMarker();
        }

        if (IsTargetReached())
        {
            DisableMarker();
        }
    }

    private void SetMarkerPosition()
    {
        _marker.transform.position = _movable.CurrentPositionTarget;
        _targetPosition = _movable.CurrentPositionTarget;

        EnableMarker();
    }

    private void EnableMarker() => _marker.gameObject.SetActive(true);

    private void DisableMarker() => _marker.gameObject.SetActive(false);

    private bool IsTargetReached() => (_marker.transform.position - _movable.CurrentPosition).magnitude <= _collectionDistance;
}
