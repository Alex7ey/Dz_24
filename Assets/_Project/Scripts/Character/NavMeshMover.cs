using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover
{
    private readonly NavMeshAgent _agent;

    public NavMeshMover(NavMeshAgent agent, float movementSpeed)
    {
        _agent = agent;
        _agent.speed = movementSpeed;
        CurrentPositionTarget = agent.transform.position;
    }

    public Vector3 CurrentPositionTarget { get; private set; }

    public void SetMovePoint(RaycastHit[] hits)
    {
        if (IsRayHitting(hits, out Vector3 targetPosition) && IsValidPath(targetPosition))
        {
            CurrentPositionTarget = targetPosition;
            _agent.SetDestination(CurrentPositionTarget);
        }
    }

    private bool IsValidPath(Vector3 targetPosition)
    {
        NavMeshPath path = new();
        _agent.CalculatePath(targetPosition, path);

        return path.status == NavMeshPathStatus.PathComplete;
    }

    private bool IsRayHitting(RaycastHit[] Hits, out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;

        foreach (RaycastHit hit in Hits)
        {
            if (hit.collider.TryGetComponent(out Ground _))
            {
                hitPoint = hit.point;
                return true;
            }
        }
        return false;
    }
}
