using System;
using UnityEngine;

public class Wp : MonoBehaviour
{
    [SerializeField]
    private Transform _bulletSpawnPosition;

    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private float _bulletDamage=1f;

    [SerializeField]
    private float _bulletSpeed=10f;

    [SerializeField]
    private float _bulletMaxFlyDistanse=10f;

    [field: SerializeField]
    public float ShootRadios { get; private set; } = 5f;

    [field: SerializeField]
    public float ShootFroquencySeconds { get; private set; } = 1f;

    [field: SerializeField]
    public ParticleSystem ShootParticle { get; private set; }

    [field: SerializeField]
    private AudioSource _shootAudioSource;

    public void Shoot(Vector3 targetPoint)
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
        ShootParticle.Play();
        _shootAudioSource.Play();

        var targetDirect = targetPoint - _bulletSpawnPosition.position;
        targetDirect.y = 0f;
        targetDirect.Normalize();

        bullet.Init(_bulletDamage, targetDirect, _bulletSpeed, _bulletMaxFlyDistanse);
    }
}
