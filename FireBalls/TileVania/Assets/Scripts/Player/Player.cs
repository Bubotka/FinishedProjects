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
    [SerializeField] private float _jumpForce=350f;

    private PlayerInput _playerInput;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private Collider2D _colider;

    private bool _isAlive;

    void Start()
    {
        _playerInput= GetComponent<PlayerInput>();
        _rigidBody= GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _colider= GetComponent<Collider2D>();
    }

    void Update()
    {
        Jump();
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

    private void Jump()
    {
        if (!_colider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            _animator.SetBool("Jump", !_colider.IsTouchingLayers(LayerMask.GetMask("Ground")));
            return;
        }

        if (_playerInput.actions["Jump"].triggered)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce);
        }
        
    }
}
