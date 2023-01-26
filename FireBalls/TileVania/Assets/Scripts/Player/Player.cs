using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed=3f;

    private PlayerInput _playerInput;
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    private bool _isAlive;

    void Start()
    {
        _playerInput= GetComponent<PlayerInput>();
        _rigidBody= GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipPlayerSprite();
    }

    private void Run()
    {
        Vector2 Input= _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 playerVelocity=new Vector2(Input.x*_moveSpeed, _rigidBody.velocity.y);
        _rigidBody.velocity = playerVelocity;

        bool playerHasSpeed = Mathf.Abs(_rigidBody.velocity.x) > 0;
        _animator.SetBool("Run", playerHasSpeed);
    }

    private void FlipPlayerSprite()
    {
        bool playerHasSpeed = Mathf.Abs(_rigidBody.velocity.x) > 0;

        if (playerHasSpeed)
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody.velocity.x), 1f);
    }
}
