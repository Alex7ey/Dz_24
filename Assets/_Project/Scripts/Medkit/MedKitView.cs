using UnityEngine;

public class MedKitView : MonoBehaviour
{
    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _colorFactor;
    [SerializeField] private float _animationSpeed;

    private Material _material;
    private PulseEffect _pulseEffect;

    private const float _minAnimationScale = 1f;
    private const float _maxAnimationScale = 2f;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;

        _pulseEffect = new(_material, this);
        _pulseEffect.StartPulseAnimation(_minAnimationScale, _maxAnimationScale, _animationSpeed);
    }
}
