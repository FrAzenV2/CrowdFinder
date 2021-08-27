using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BotMovement : MovingBehaviour
{
    [SerializeField] bool doWander = true;
    [SerializeField] float wanderRadius = 2.0f;
    [SerializeField] float wanderTime = 1.0f;
    [SerializeField] float wanderTimeRandom = 0.25f;
    [SerializeField] float followUpdateTime = 0.15f;
    [SerializeField] float followMinDistance = 1.4f;
    
    private Vector2 _startPosition;
    private Transform _followTarget;
    private bool _following;


    public void StartFollowing(Transform target)
    {
        doWander = false;
        _following = true;
        _followTarget = target;
        _currentStopDistance = followMinDistance;
        StartCoroutine(FollowCoroutine());
    }

    public void StopFollowing()
    {
        doWander = true;
        _following = false;
        _followTarget = null;
        _currentStopDistance = _stopBaseDistance;
        StartCoroutine(WanderCoroutine());
    }

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
            yield return new WaitForSeconds(wanderTime + Random.Range(-wanderTimeRandom, wanderTimeRandom));
            Wander();
        }
    }

    IEnumerator FollowCoroutine()
    {
        while (_following && _followTarget!=null)
        {
            MoveAt(_followTarget.position);
            yield return new WaitForSeconds(followUpdateTime);
        }
    }
}
