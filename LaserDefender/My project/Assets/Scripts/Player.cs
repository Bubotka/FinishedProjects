using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private int _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _padding=0.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _laserSpeed;
    [SerializeField] private float _projectileFiringPeriod=0.1f;
    [Header("Sounds")]
    [SerializeField] private float _deathSoundVolume = 0.75f;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _shootsSounds;
    [SerializeField] private float _shootsSoundsVolume=0.3f;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    private Coroutine _firingCoroutine;

    private void Start()
    {
        Camera gameCamer = Camera.main;
        _minX= gameCamer.ViewportToWorldPoint(new Vector3(0,0,0)).x+_padding;
        _maxX = gameCamer.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _padding;
        _minY = gameCamer.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _padding;
        _maxY = gameCamer.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _padding;
    }

    void Update() 
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine=StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _laserSpeed);
            AudioSource.PlayClipAtPoint(_shootsSounds, Camera.main.transform.position, _shootsSoundsVolume);
            yield return new WaitForSeconds(_projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
        var deltaY = Input.GetAxis("Vertical")*Time.deltaTime * _moveSpeed;
        transform.position=new Vector2(Mathf.Clamp(transform.position.x+deltaX,_minX,_maxX)
            ,Mathf.Clamp(transform.position.y+deltaY,_minY,_maxY));

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
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(_deathSound, Camera.main.transform.position, _deathSoundVolume);
        FindObjectOfType<Level>().LoadGameOverScreen();
    }

    public int GetHealth()
    {
        return _health;
    }
}
