using UnityEngine;

public class PointClickMovementController : Controller
{
    private readonly IMovable _movable;
    private const int LeftMouseButton = 0;

    public PointClickMovementController(IMovable movable)
    {
        _movable = movable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(LeftMouseButton) && TryGetHits(out RaycastHit[] Hits))
        {
            _movable.MoveTo(Hits);
        }
    }

    private bool TryGetHits(out RaycastHit[] Hits)
    {
        Hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));

        foreach (RaycastHit hit in Hits)
        {
            if (hit.point != null)
                return true;
        }

        return false;
    }
}
