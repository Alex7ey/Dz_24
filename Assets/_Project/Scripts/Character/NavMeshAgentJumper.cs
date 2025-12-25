using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentJumper
{
    private readonly float _jumpSpeed;
    private readonly NavMeshAgent _agent;
    private readonly MonoBehaviour _corutineRunner;
    private readonly AnimationCurve _animationCurve;
    private const float JumpHeight = 2f;

    private Coroutine _jumpCoroutine;

    public bool InProcess => _jumpCoroutine != null;

    public NavMeshAgentJumper(float jumpSpeed, NavMeshAgent agent, MonoBehaviour corutineRunner, AnimationCurve animationCurve)
    {
        _jumpSpeed = jumpSpeed;
        _agent = agent;
        _corutineRunner = corutineRunner;
        _animationCurve = animationCurve;
        _agent.autoTraverseOffMeshLink = false;
    }

    public void Jump(OffMeshLinkData offMeshLink, Vector3 targetPoint)
    {
        if (InProcess == false)
        {
            _jumpCoroutine = _corutineRunner.StartCoroutine(JumpProcess(offMeshLink, targetPoint));
        }
    }

    private IEnumerator JumpProcess(OffMeshLinkData offMeshLink, Vector3 targetPoint)
    {
        BeginJump();

        Vector3 startPos = _agent.transform.position;
        Vector3 endPos = offMeshLink.endPos;

        float progress = 0f;
        float duration = Vector3.Distance(startPos, endPos) / _jumpSpeed;
        float yOffSet;

        while (progress <= duration)
        {
            yOffSet = _animationCurve.Evaluate(progress / duration);
            _agent.transform.position = Vector3.Lerp(startPos, endPos, progress / duration) + (JumpHeight * yOffSet * Vector3.up);
            progress += Time.deltaTime;
            yield return null;
        }

        FinishJump();
    }

    private void BeginJump()
    {
        _agent.isStopped = true;
    }

    private void FinishJump()
    {
        _agent.CompleteOffMeshLink();
        _agent.isStopped = false;
        _jumpCoroutine = null;
    }
}
