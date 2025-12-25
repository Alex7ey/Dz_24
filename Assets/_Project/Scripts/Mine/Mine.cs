using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Mine : MonoBehaviour
{
    [SerializeField] private float _timeBeforeExplosion;
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private ShaderPulseController _shader;

    private float _detectionRadius;
    private Explosion _explosion;

    private float _sizeMultiplier => transform.localScale.x;

    private void Awake()
    {
        _detectionRadius = GetComponent<SphereCollider>().radius * _sizeMultiplier;
        _explosion = new(_timeBeforeExplosion, transform, _detectionRadius, _explosionEffect, _damage, this, _explosionSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable _))
        {
            _explosion.Explode();
            _shader.StartCoroutine(_shader.PlayAnimationExplosion());
        }
    }

    private void OnDrawGizmos()
    {
        ShowRadiusDetection();
    }

    private void ShowRadiusDetection()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, _detectionRadius);
    }
}
