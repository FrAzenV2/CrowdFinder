using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovingBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    Vector2 _pointerPosition;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void AnimationsUpdate(){
        sprite.flipX = _targetDirection.x <= 0;
    }

    // Callbacks
    void OnClick(InputValue value)
    {
        Vector2 worldPosition = (Vector2) Camera.main.ScreenToWorldPoint(_pointerPosition);
        MoveAt(worldPosition);
    }

    void OnPointerPosition(InputValue value)
    {
        _pointerPosition = value.Get<Vector2>();
    }
}
