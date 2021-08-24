using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickInteractor : MonoBehaviour
{
    public UnityAction OnClicked;

    public void Click(){
        OnClicked.Invoke();
    }
}