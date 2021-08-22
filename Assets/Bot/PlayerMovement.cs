using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovingBehaviour
{

    Vector2 _pointerPosition;

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Callbacks
    void OnClick(InputValue value){
        Vector2 worldPosition = (Vector2) Camera.main.ScreenToWorldPoint(_pointerPosition);
        MoveAt(worldPosition);
    }

    void OnPointerPosition(InputValue value){
        _pointerPosition = value.Get<Vector2>();
    }
}
