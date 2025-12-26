using System.Collections;
using UnityEngine;

public class DissolveEffect 
{

    private readonly SkinnedMeshRenderer[] _skinnedMeshRenderers;
    private readonly MonoBehaviour _coroutineRunner;
    private readonly float _dissolveTime = 2f;

    private const string EdgeKey = "_Edge";

    private const float MinValue = 0;
    private const float MaxValue = 1;

    public DissolveEffect(SkinnedMeshRenderer[] skinnedMeshRenderers, MonoBehaviour coroutineRunner, float dissolveTime)
    {
        _skinnedMeshRenderers = skinnedMeshRenderers;
        _coroutineRunner = coroutineRunner;
        _dissolveTime = dissolveTime;
    }

    public void PlayDissolveEffect()
    {
        _coroutineRunner.StartCoroutine(ProcessDissolve());
    }

    public IEnumerator ProcessDissolve()
    {
        float progress = 0;

        while (progress < _dissolveTime)
        {
            progress += Time.deltaTime;

            float value = Mathf.Lerp(MinValue, MaxValue, progress / _dissolveTime);

            SetValueToShader(value);

            yield return null;
        }
    }

    private void SetValueToShader(float value)
    {
        foreach (SkinnedMeshRenderer skin in _skinnedMeshRenderers)
        {
            Material[] materials = skin.materials;

            foreach (Material mat in materials)
            {
                mat.SetFloat(EdgeKey, value);
            }
        }
    }
}
