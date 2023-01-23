using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle _paddle1;
    [SerializeField] private float _xPush=2f;
    [SerializeField] private float _yPush=15f;
    [SerializeField] private AudioClip[] _ballSounds;
    [SerializeField] private float _randomFactor = 2f;

    private Vector2 _paddleToBallVector;
    private bool _hasStarted;
    private AudioSource _audioSource;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _paddleToBallVector= transform.position-_paddle1.transform.position;
        _audioSource=GetComponent<AudioSource>();
        _rigidBody=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!_hasStarted)
            LockBallToPaddle();
        LaunchOnMouseClick();
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hasStarted = true;
            _rigidBody.GetComponent<Rigidbody2D>().velocity = new Vector2(_xPush, _yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 PaddlePosition = new Vector2(_paddle1.transform.position.x, _paddle1.transform.position.y);
        transform.position = _paddleToBallVector + PaddlePosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweek=new Vector2
            (UnityEngine.Random.Range(0,_randomFactor),
            UnityEngine.Random.Range(0,_randomFactor));
       if(_hasStarted)
        {
            AudioClip clip = _ballSounds[UnityEngine.Random.Range(0,_ballSounds.Length)];
            _audioSource.PlayOneShot(clip);
            _rigidBody.velocity += velocityTweek;
        }
    }
}
