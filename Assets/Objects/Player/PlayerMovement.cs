using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MovingBehaviour
{
    private Vector2 _pointerPosition;
    private bool _isPlayerInputBlocked;
    public void BlockPlayerInput()
    {
        _isPlayerInputBlocked = true;
    }

    public void UnblockPlayerInput()
    {
        _isPlayerInputBlocked = false;
    }

    private void OnClick()
    {
        if(_isPlayerInputBlocked) return;
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