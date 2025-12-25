using System.Collections;
using UnityEngine;

public class ShaderAnimator : MonoBehaviour
{
    [SerializeField] private float _dissolveTime;

    private SkinnedMeshRenderer[] _skinnedMeshRenderers;

    private const string EdgeKey = "_Edge";

    private const float MinValue = 0;
    private const float MaxValue = 1;

    private void Awake()
    {
        _skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public IEnumerator PlayDissolveEffect()
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
