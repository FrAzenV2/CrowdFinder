using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BotMovement : MovingBehaviour
{
    [SerializeField] SpriteRenderer sprite;

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
}
