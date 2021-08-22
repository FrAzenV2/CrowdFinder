using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovingBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    private Vector2 _pointerPosition;

    protected override void Start()
    {
        base.Start();
    }

    protected override void AnimationsUpdate(){
        if (_targetDirection.x > 0)
            sprite.flipX = false;
        else if (_targetDirection.x < 0)
            sprite.flipX = true;
    }

    // Callbacks
    void OnClick()
    {
        Vector2 worldPosition = (Vector2) Camera.main.ScreenToWorldPoint(_pointerPosition);
        MoveAt(worldPosition);
    }

    void OnCancelMove(){
        Stop();
    }

    void OnPointerPosition(InputValue value)
    {
        _pointerPosition = value.Get<Vector2>();
    }
}
