using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EventChannels;
using Dialogs;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
    private PlayerMovement _playerMovement;

    private void Awake() {
        _playerMovement = GetComponent<PlayerMovement>();
        _dialogEventChannel.OnDialogOpened += OnDialogOpened;
        _dialogEventChannel.OnDialogClosed += OnDialogClosed;
    }

    private void OnDialogOpened(DialogSO dialog)
    {
        _playerMovement.Freeze();
    }

    private void OnDialogClosed()
    {
        _playerMovement.Unfreeze();
    }
}