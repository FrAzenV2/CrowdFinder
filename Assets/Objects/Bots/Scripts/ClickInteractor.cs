using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickInteractor : MonoBehaviour
{
    public UnityAction OnClicked;
    public UnityAction OnReleased;

    public void Click()
    {
        if (OnClicked != null)
            OnClicked.Invoke();
    }

    public void Release()
    {
        if (OnReleased != null)
            OnReleased.Invoke();
    }
}