using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Objects.Player;

using EventChannels;

public class GameManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Player _player;

    [Header("UI")]
    [SerializeField] private GameObject _confettiParticles;
    [SerializeField] private TMP_Text _timerText;

    [Header("Events")]
    [SerializeField] private GameEventChannelSO _gameEventChannel; 

    private void Awake() {
        _gameEventChannel.OnGameStarted += OnGameStarted;
        _gameEventChannel.OnGameWon += OnGameWon;
    }

    private void OnGameStarted()
    {

    }

    private void OnGameWon()
    {
        Instantiate(_confettiParticles, _player.transform.position, Quaternion.identity);
    }

    private bool _gameStarted = false;
    private float _gameTime = 0f;
}
