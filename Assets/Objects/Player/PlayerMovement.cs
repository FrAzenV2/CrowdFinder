using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovingBehaviour
{
    private Vector2 _pointerPosition;

    protected override void Start()
    {
        base.Start();
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
