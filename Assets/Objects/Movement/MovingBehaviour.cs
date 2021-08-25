using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingBehaviour : MonoBehaviour, IMoving
{
    [Header("Movement Settings")] [SerializeField]
    private float movementSpeed = 4.5f;

    [SerializeField] private float movementSmooth = 24.0f;
    [SerializeField] private float stopDistance = 0.25f;

    [Header("Visual Settings")] [SerializeField]
    private SpriteRenderer sprite;

    protected bool _isMoving;
    protected bool _isFrozen;
    protected float _currentSpeed;
    protected Vector2 _targetDirection;
    protected Vector2 _targetPosition;

    private Rigidbody2D _rb;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Stop();
    }

    protected virtual void Update()
    {
        AnimationsUpdate();
    }

    protected virtual void FixedUpdate()
    {
        // Check distances
        _isMoving = Vector2.Distance(transform.position, _targetPosition) > stopDistance;

        // Update direction
        _targetDirection = (_targetPosition - (Vector2) transform.position).normalized;

        // Update velocity
        var targetSpeed = _isMoving ? movementSpeed : 0f;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * movementSmooth);
        _rb.velocity = _targetDirection * _currentSpeed;
    }

    protected virtual void AnimationsUpdate()
    {
        if (_targetDirection.x > 0)
            sprite.flipX = false;
        else if (_targetDirection.x < 0)
            sprite.flipX = true;
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
}