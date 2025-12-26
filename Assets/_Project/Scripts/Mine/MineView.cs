using System.Collections;
using UnityEngine;

public class MineView : MonoBehaviour
{
    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _colorFactor;
    [SerializeField] private float _animationSpeed;

    private Mine _mine;
    private Material _material;
    private Coroutine _coroutine;
    private PulseEffect _pulseEffect;

    private const float _minAnimationScale = 1f;
    private const float _maxAnimationScale = 2f;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _mine = GetComponent<Mine>();
        _pulseEffect = new(_material, this);
    }

    private void Update()
    {
        if (_mine.IsExploding && _coroutine == null)
            _coroutine = _pulseEffect.StartPulseAnimation(_minAnimationScale, _maxAnimationScale, _animationSpeed);
    }
}


