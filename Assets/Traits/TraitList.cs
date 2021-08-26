using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventChannels;
using Traits;

public class TraitList : MonoBehaviour
{
    [SerializeField] private TraitEventChannelSO _traitEventChannel = default;
    [SerializeField] private TraitListEntry _traitListEntryPrefab;

    private void Awake()
    {
        _traitEventChannel.OnTraitGenerated += OnTraitGenerated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTraitGenerated(ITrait trait)
    {
        if (_traitList.Contains(trait))
            return;

        TraitListEntry listEntry = Instantiate(_traitListEntryPrefab, Vector3.zero, Quaternion.identity, transform);
        listEntry.Initialize(trait);
        _traitList.Add(trait);
    }

    private List<ITrait> _traitList = new List<ITrait>();
}
