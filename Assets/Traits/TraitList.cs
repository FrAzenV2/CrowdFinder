using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventChannels;
using Traits;
using TMPro;

public class TraitList : MonoBehaviour
{
    [SerializeField] private TraitEventChannelSO _traitEventChannel = default;
    [SerializeField] private TraitListEntry _traitListEntryPrefab;
    [SerializeField] private RectTransform _traitHolder;
    [SerializeField] private RectTransform _traitGroup;
    [SerializeField] private TMP_Text _toggleButtonText;
    [SerializeField] private string[] _toggleButtonLabels = {"Show Trait List", "Hide Trait List"};

    private void Awake()
    {
        _traitEventChannel.OnTraitGenerated += OnTraitGenerated;
        _traitEventChannel.OnTraitRemoved += OnTraitRemoved;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTraitGenerated(ITrait trait)
    {
        if (_traitList.Contains(trait))
            return;
        TraitListEntry listEntry = Instantiate(_traitListEntryPrefab, Vector3.zero, Quaternion.identity, _traitGroup);
        listEntry.Initialize(trait);
        _traitList.Add(trait);
    }

    private void OnTraitRemoved(ITrait trait)
    {
        _traitList.Remove(trait);
    }

    public void ToggleListVisibility()
    {
        _isListVisible = !_isListVisible;
        _toggleButtonText.text = _toggleButtonLabels[_isListVisible ? 1 : 0];
        _traitHolder.gameObject.SetActive(_isListVisible);
    }

    private List<ITrait> _traitList = new List<ITrait>();
    private bool _isListVisible = false;
}
