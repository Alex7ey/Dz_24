using System.Collections;
using UnityEngine;

public class Explosion
{
    private readonly int _damage;
    private readonly float _radiusExplode;
    private readonly float _timeBeforeExplosion;

    private readonly Transform _transform;
    private readonly ParticleSystem _particleEffect;
    private readonly MonoBehaviour _coroutineRunner;
    private readonly AudioClip _explosionAudioClip;

    private bool _isDestroyed;
    private Coroutine _coroutineCountDown;

    public Explosion(float timeBeforeExplosion, Transform transform, float radiusExplode, ParticleSystem particleEffect, int damage, MonoBehaviour coroutineRunner, AudioClip explosionAudioClip)
    {
        _timeBeforeExplosion = timeBeforeExplosion;
        _transform = transform;
        _radiusExplode = radiusExplode;
        _particleEffect = particleEffect;
        _damage = damage;
        _coroutineRunner = coroutineRunner;
        _explosionAudioClip = explosionAudioClip;
    }

    public bool IsExploding => _coroutineCountDown != null;

    public void Explode()
    {
        if (IsExploding || _isDestroyed == true)
            return;

        _coroutineCountDown = _coroutineRunner.StartCoroutine(ProcessExplosionCountdown());
    }

    private IEnumerator ProcessExplosionCountdown()
    {
        yield return new WaitForSeconds(_timeBeforeExplosion);

        ExecuteExplosion();

        _isDestroyed = true;
        _coroutineCountDown = null;

        yield break;
    }

    private void ExecuteExplosion()
    {
        PlayExplossionEffect();
        PlayExplosionSound();
        DealDamage();
        DisableObject();
    }

    private void PlayExplossionEffect()
    {
        _particleEffect.transform.position = _transform.position;
        _particleEffect.Play();
    }

    private void PlayExplosionSound() => AudioHandler.PlaySound(_explosionAudioClip);

    private void DisableObject() => _transform.gameObject.SetActive(false);

    private void DealDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _radiusExplode);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(_damage);
        }
    }
}
