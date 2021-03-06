using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingBehaviour : MonoBehaviour, IMoving
{
    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed = 4.5f;

    [SerializeField] private float _movementSmooth = 24.0f;
    [SerializeField] protected float _stopBaseDistance = 0.25f;

    [Header("Visual Settings")]
    [SerializeField] private Transform _spriteHolder;
    [SerializeField] private Animator _animator;

    protected bool _isMoving;
    protected bool _isFrozen;
    protected float _currentSpeed;
    protected Vector2 _targetDirection;
    protected Vector2 _targetPosition;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialSpriteScale = _spriteHolder.localScale;
        _currentStopDistance = _stopBaseDistance;
        Stop();
    }

    protected virtual void Update()
    {
        AnimationsUpdate();
    }
    
    
    protected virtual void FixedUpdate()
    {
        // Check distances
        _isMoving = Vector2.Distance(transform.position, _targetPosition) > _currentStopDistance;

        // Update direction
        _targetDirection = (_targetPosition - (Vector2) transform.position).normalized;

        // Update velocity
        var targetSpeed = _isMoving ? _movementSpeed : 0f;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * _movementSmooth);
        if (_currentSpeed < 1e-2f)
        {
            _currentSpeed = 0;
        }

        _rb.velocity = _targetDirection * _currentSpeed;
    }

    protected virtual void AnimationsUpdate()
    {
        if (!_isFrozen){
            if (_targetDirection.x > 0)
                FlipSprite(false);
            else if (_targetDirection.x < 0)
                FlipSprite(true);
        }
        //float speed = _rb.velocity.magnitude;
        //if (speed < 0.15f)
        //    speed = 0f;
        _animator.SetFloat("Speed", _currentSpeed);
    }

    public void MoveAt(Vector2 point)
    {
        if (_isFrozen)
            return;
        _targetPosition = point;
    }

    public void Stop()
    {
        _targetPosition = transform.position;
    }

    public void DisableMovement(bool stop = true)
    {
        _isFrozen = true;
        if(stop)
            Stop();
    }

    public void EnableMovement()
    {
        _isFrozen = false;
    }

    public void FlipSprite(bool flip = false){
        Vector3 scale = _spriteHolder.localScale;
        scale.x = flip ? -_initialSpriteScale.x : _initialSpriteScale.x;
        _spriteHolder.localScale = scale;
    }

    protected float _currentStopDistance;
    private Vector3 _initialSpriteScale;
    private Rigidbody2D _rb;
}