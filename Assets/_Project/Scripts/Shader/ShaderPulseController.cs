using System.Collections;
using UnityEngine;

public class ShaderPulseController : MonoBehaviour
{
    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _colorFactor;

    private Material _material;

    private const string ColorKey = "_ValueColor";
    private const string ScaleKey = "_ScaleFactor";

    private float _startSize => transform.localScale.x;


    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    public void PlayExplosionAnimation()
    {
        StartCoroutine(PlayAnimationExplosion());
    }

    public IEnumerator PlayAnimationExplosion()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime * 1.5f;

            float value = Mathf.PingPong(time, 1);

            _material.SetFloat(ColorKey, value * _colorFactor);
            //_material.SetFloat(ScaleKey, value + _startSize);
            _material.SetFloat(ScaleKey, _startSize);

            yield return null;
        }
    }
}


