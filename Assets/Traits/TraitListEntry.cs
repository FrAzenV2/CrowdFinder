using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traits;
using TMPro;
using EventChannels;

public class TraitListEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TraitEventChannelSO _traitEventChannel;
    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    public void Initialize(ITrait trait){
        _trait = trait;
        _text.text = $"<b>{trait.Sender.Config.BotName}</b>: {trait.GetTraitText()}";
    }

    public void Close(){
        _animator.Play("Hide");
        _traitEventChannel.RemoveTrait(_trait);
    }

    public void Remove(){
        Destroy(gameObject);
    }

    private ITrait _trait;
    private Animator _animator;
    
}
