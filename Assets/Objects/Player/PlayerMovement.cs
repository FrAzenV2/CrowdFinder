using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MovingBehaviour
{

    private Vector2 _pointerPosition;
    
    protected override void Start()
    {
        base.Start();
    }

    // Callbacks
    private void OnClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        var worldPosition = (Vector2) Camera.main.ScreenToWorldPoint(_pointerPosition);
        MoveAt(worldPosition);
    }

    private void OnCancelMove()
    {
        Stop();
    }

    private void OnPointerPosition(InputValue value)
    {
        _pointerPosition = value.Get<Vector2>();
    }
}