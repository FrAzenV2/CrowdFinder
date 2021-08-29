using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactLayers;
    public UnityAction<GameObject> OnZoneEntered;
    public UnityAction<GameObject> OnZoneExited;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, _interactLayers))
            OnZoneEntered?.Invoke(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, _interactLayers))
            OnZoneExited?.Invoke(other.gameObject);
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }
}