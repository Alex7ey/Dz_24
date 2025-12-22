using UnityEngine;
using UnityEngine.AI;

public class TargetToPointRotator
{
    private readonly Transform _transform;
    private readonly float _rotationSpeed;
    private readonly NavMeshAgent _agent;

    private Vector3 _lookAtPosition;

    private NavMeshPath _path = new();

    public TargetToPointRotator(Transform transform, float rotationSpeed, NavMeshAgent agent)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
        _agent = agent;
        _agent.updateRotation = false;

        SetLookAtPosition(transform.position + transform.forward);
    }

    public void SetLookAtPosition(Vector3 point) => _lookAtPosition = point;

    public void Update(float deltaTime)
    {
        if (CanRotate())
            ProcessRotate(deltaTime);
    }

    private void ProcessRotate(float deltaTime)
    {
        Vector3 direction = GetTargetRotatePoint() - _transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion angle = Quaternion.LookRotation(direction);
            float stepRotation = _rotationSpeed * deltaTime;
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, angle, stepRotation);
        }
    }

    private bool CanRotate() => Vector3.Distance(_transform.position, _lookAtPosition) > _agent.stoppingDistance;

    private Vector3 GetTargetRotatePoint()
    {
        _agent.CalculatePath(_lookAtPosition, _path);

        if (_path.corners.Length > 0)
        {
            return _path.corners[1];
        }

        return _lookAtPosition;
    }
}
