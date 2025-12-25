using System.Collections;
using UnityEngine;

public class ViewMedKit : MonoBehaviour
{
    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _colorFactor;
    [SerializeField] private float _animationSpeed;

    private Material _material;

    private const string ShaderColorKey = "_ColorFactor";
    private const string ShaderScaleKey = "_ScaleFactor";

    private const int MinScale = 1;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        float elapsedTime = 0;

        while (true)
        {
            elapsedTime += Time.deltaTime * _animationSpeed;

            float resultPingPong = Mathf.PingPong(elapsedTime, 1);

            _material.SetFloat(ShaderColorKey, resultPingPong * _colorFactor);
            _material.SetFloat(ShaderScaleKey, resultPingPong + MinScale);

            yield return null;
        }
    }
}
