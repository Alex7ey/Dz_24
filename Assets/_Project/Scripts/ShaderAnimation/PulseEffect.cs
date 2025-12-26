using System.Collections;
using UnityEngine;

public class PulseEffect
{
    private readonly Material _material;
    private readonly MonoBehaviour _coroutineRunner;

    private const string ShaderColorKey = "_ColorValue";
    private const string ShaderScaleKey = "_ScaleFactor";

    public PulseEffect(Material material, MonoBehaviour coroutineRunner)
    {
        _material = material;
        _coroutineRunner = coroutineRunner;
    }

    public Coroutine StartPulseAnimation(float minValue, float maxValue, float animationSpeed)
    {
        return _coroutineRunner.StartCoroutine(CustomPingPong(minValue, maxValue, animationSpeed));
    }

    private IEnumerator CustomPingPong(float minValue, float maxValue, float animationSpeed)
    {
        float range = maxValue - minValue;
        float duration = range / animationSpeed;

        float elapsedTime = 0f;

        while (true)
        {
            while (elapsedTime < duration)
            {
                float percent = elapsedTime / duration;
                float result = Mathf.Lerp(minValue, maxValue, percent);

                elapsedTime += Time.deltaTime;

                ApplyMaterialValues(result, percent);

                yield return null;
            }

            while (elapsedTime > 0)
            {
                float percent = elapsedTime / duration;
                float result = Mathf.Lerp(minValue, maxValue, percent);

                elapsedTime -= Time.deltaTime;

                ApplyMaterialValues(result, percent);

                yield return null;
            }

            elapsedTime = 0f;
            yield return null;
        }
    }

    private void ApplyMaterialValues(float value, float percent)
    {
        _material.SetFloat(ShaderScaleKey, value);
        _material.SetFloat(ShaderColorKey, percent);
    }
}
