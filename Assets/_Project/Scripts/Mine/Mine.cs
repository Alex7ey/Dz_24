using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Mine : MonoBehaviour
{
    [SerializeField] private float _timeBeforeExplosion;
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioClip _explosionSound;

    private float _detectionRadius;
    private Explosion _explosion;

    private float _scaleFactor => transform.localScale.x;
    public bool IsExploding  => _explosion.IsExploding;

    private void Awake()
    {
        _detectionRadius = GetComponent<SphereCollider>().radius * _scaleFactor;
        _explosion = new(_timeBeforeExplosion, transform, _detectionRadius, _explosionEffect, _damage, this, _explosionSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable _))
        {
            _explosion.Explode();
        }
    }

    private void OnDrawGizmos()
    {
        ShowRadiusDetection();
    }

    private void ShowRadiusDetection()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, _detectionRadius);
    }
}
