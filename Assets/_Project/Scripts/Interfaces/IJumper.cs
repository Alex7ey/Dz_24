using UnityEngine.AI;

public interface IJumper
{ 
    NavMeshAgent Agent { get; }

    void Jump(OffMeshLinkData offMeshLinkData);
}
