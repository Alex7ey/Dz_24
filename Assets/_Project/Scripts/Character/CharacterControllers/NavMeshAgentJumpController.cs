using UnityEngine.AI;

public class NavMeshAgentJumpController : Controller
{
    private readonly IJumper _jumper;
    private readonly NavMeshAgent _agent;

    public NavMeshAgentJumpController(IJumper jumper, NavMeshAgent navMeshAgent)
    {
        _jumper = jumper;
        _agent = navMeshAgent;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (IsOnNavMeshLink(out OffMeshLinkData navMeshLinkData))
            _jumper.Jump(navMeshLinkData);
    }

    private bool IsOnNavMeshLink(out OffMeshLinkData navMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            navMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        navMeshLinkData = default;
        return false;
    }
}
