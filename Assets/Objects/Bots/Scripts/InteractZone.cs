using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractZone : MonoBehaviour
{
    [SerializeField] private LayerMask _interactLayers;
    public UnityAction<GameObject> OnZoneEntered;
    public UnityAction<GameObject> OnZoneExited;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnZoneEntered.Invoke(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnZoneExited.Invoke(other.gameObject);
    }
}