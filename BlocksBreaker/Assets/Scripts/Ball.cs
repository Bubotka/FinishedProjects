using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle _paddle1;
    [SerializeField] private float _xPush=2f;
    [SerializeField] private float _yPush=15f;

    private Vector2 _paddleToBallVector;
    private bool _hasStarted;

    void Start()
    {
        _paddleToBallVector= transform.position-_paddle1.transform.position;
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(_xPush, _yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 PaddlePosition = new Vector2(_paddle1.transform.position.x, _paddle1.transform.position.y);
        transform.position = _paddleToBallVector + PaddlePosition;
    }
}
