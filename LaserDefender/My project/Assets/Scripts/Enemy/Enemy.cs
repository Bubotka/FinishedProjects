using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private int _health = 100;
    [SerializeField] private float _minTimeBetweenShots = 0.2f;
    [SerializeField] private float _maxTimeBetweenShots = 2f;
    [SerializeField] private GameObject _enemyLaser;
    [SerializeField] private float _enemyLaserSpeed;
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private int _scoreValue=10;
    [Header("Sounds")]
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _deathSoundVolume=0.75f;
    [SerializeField] private AudioClip _ShootsSounds;
    [SerializeField] private float _ShootsSoundsVolume = 0.3f;

    private float _shootCounter;

    private void Start()
    {
        _shootCounter= Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    private void Update()
    {
        CountAndShoot();
    }

    private void CountAndShoot()
    {
        _shootCounter-= Time.deltaTime;

        if (_shootCounter <= 0)
        {
            Fire();
            _shootCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_ShootsSounds, Camera.main.transform.position, _ShootsSoundsVolume);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_enemyLaserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(_scoreValue);
        Destroy(gameObject);
        GameObject _explosion = Instantiate(_deathVFX, transform.position, transform.rotation);
        Destroy(_explosion,1f);
        AudioSource.PlayClipAtPoint(_deathSound, Camera.main.transform.position, _deathSoundVolume);

    }
}
