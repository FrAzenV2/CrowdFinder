using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BotMovement : MovingBehaviour
{
    [SerializeField] bool doWander = true;
    [SerializeField] float wanderRadius = 2.0f;
    [SerializeField] float wanderTime = 1.0f;
    [SerializeField] float wanderTimeRandom = 0.25f;

    private Vector2 _startPosition;

    protected override void Start()
    {
        base.Start();
        _startPosition = transform.position;
        StartCoroutine(WanderCoroutine());
    }

    void Wander(){
        Vector2 wanderPosition = _startPosition + Random.insideUnitCircle * wanderRadius;
        MoveAt(wanderPosition);
    }

    // Coroutines
    IEnumerator WanderCoroutine(){
        while (doWander){
            Wander();
            yield return new WaitForSeconds(wanderTime + Random.Range(-wanderTimeRandom, wanderTimeRandom));
        }
    }

}
