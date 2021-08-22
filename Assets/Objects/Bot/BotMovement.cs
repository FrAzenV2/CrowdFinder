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
}
