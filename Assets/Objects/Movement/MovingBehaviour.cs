using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingBehaviour : MonoBehaviour, IMoving
{
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float movementSmooth = 24.0f;
    [SerializeField] float stopDistance = 0.1f;

    protected bool _isMoving;
    protected float _currentSpeed;
    protected Vector2 _targetDirection;
    protected Vector2 _targetPosition;

    private Rigidbody2D _rb;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    protected virtual void Update(){
        AnimationsUpdate();
    }

    protected virtual void FixedUpdate()
    {
        // Check distances
        _isMoving = Vector2.Distance(transform.position, _targetPosition) > stopDistance;

        // Update direction
        _targetDirection = (_targetPosition - (Vector2) transform.position).normalized;

        // Update velocity
        float targetSpeed = _isMoving ? movementSpeed : 0f;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * movementSmooth);
        _rb.velocity = _targetDirection * _currentSpeed;
    }

    protected virtual void AnimationsUpdate()
    {

    }

    public void MoveAt(Vector2 point)
    {
        _targetPosition = point;
    }

    public void Stop(){
        _targetPosition = transform.position;
    }
}