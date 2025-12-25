using UnityEngine.AI;

public class NavMeshAgentJumpController : Controller
{
    private readonly IJumper _jumper;

    public NavMeshAgentJumpController(IJumper jumper)
    {
        _jumper = jumper;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (IsOnNavMeshLink(out OffMeshLinkData navMeshLinkData))
            _jumper.Jump(navMeshLinkData);
    }

    private bool IsOnNavMeshLink(out OffMeshLinkData navMeshLinkData)
    {
        if (_jumper.Agent.isOnOffMeshLink)
        {
            navMeshLinkData = _jumper.Agent.currentOffMeshLinkData;
            return true;
        }

        navMeshLinkData = default;
        return false;
    }
}
