using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointAndClickInteractor : MonoBehaviour
{
    [SerializeField] private ClickInteractor _clickInteractor;
    [SerializeField] private CollisionInteractor _collisionInteractor;
    [SerializeField] private GameObject _highlight;
    
    [HideInInspector] public GameObject InteractingObject;
    public UnityAction OnHighlighted;
    public UnityAction OnDehighlighted;
    public UnityAction OnStartedInteraction;
    public UnityAction OnEndedInteraction;

    private void Awake() {
        _clickInteractor.OnClicked += OnClicked;
        _clickInteractor.OnReleased += OnReleased;
        _collisionInteractor.OnZoneEntered += OnZoneEntered;
        _collisionInteractor.OnZoneExited += OnZoneExited;
        SetState(State.IDLE);
    }

    private void OnClicked()
    {
        switch (_state)
        {
            case State.IDLE:
                SetState(State.WAITING);
                break;
            case State.HIGHLIGHTED:
                SetState(State.INTERACTING);
                break;
            case State.INTERACTING:
                SetState(State.HIGHLIGHTED);
                break;
        }
    }

    private void OnReleased()
    {
        switch (_state)
        {
            case State.INTERACTING:
                SetState(State.HIGHLIGHTED);
                break;
            case State.WAITING:
                SetState(State.IDLE);
                break;
        }
    }

    private void OnZoneEntered(GameObject obj)
    {
        _objectInZone = true;
        InteractingObject = obj;
        if (_state == State.IDLE)
            SetState(State.HIGHLIGHTED);
        else if (_state == State.WAITING)
            SetState(State.INTERACTING);
    }

    private void OnZoneExited(GameObject obj)
    {
        _objectInZone = false;
        InteractingObject = null;
        if (_state == State.HIGHLIGHTED || _state == State.INTERACTING)
            SetState(State.IDLE);
    }

    private void SetState(State newState)
    {
        switch (newState)
        {
            case State.IDLE:
                SetHighlightVisibility(false);
                if (_state == State.INTERACTING)
                    EndInteraction();
                break;
            case State.WAITING:
                SetHighlightVisibility(true);
                break;
            case State.HIGHLIGHTED:
                SetHighlightVisibility(true);
                if (_state == State.INTERACTING)
                    EndInteraction();
                break;
            case State.INTERACTING:
                if (_state == State.HIGHLIGHTED || _state == State.WAITING)
                    StartInteraction();
                break;
        }
        _state = newState;
    }

    private void EndInteraction()
    {
        OnEndedInteraction.Invoke();
    }

    private void StartInteraction()
    {
        OnStartedInteraction.Invoke();
    }

    private void SetHighlightVisibility(bool visible)
    {
        _highlight.SetActive(visible);
    }

    enum State
    {
        IDLE,
        HIGHLIGHTED,
        WAITING,
        INTERACTING
        
    }
    private bool _objectInZone = false;
    private State _state = State.IDLE;
}