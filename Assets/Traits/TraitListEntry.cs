using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traits;
using TMPro;

public class TraitListEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private void Awake() {
        
    }
    public void Initialize(ITrait trait){
        _trait = trait;
        _text.text = $"{trait.Sender.Config.BotName}: {trait.GetTraitText()}";
    }

    private ITrait _trait;
    
}
