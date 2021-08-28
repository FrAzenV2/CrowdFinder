using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Objects.Player;

using EventChannels;

public class GameManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _maxGameTime = 180f;

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
        _gameEventChannel.OnGameLost += OnGameLost;
    }

    private void OnGameStarted()
    {
        _gameStarted = true;
        _gameTimer = _maxGameTime;
    }

    private void OnGameWon()
    {
        Instantiate(_confettiParticles, _player.transform.position, Quaternion.identity);
    }

    private void OnGameLost()
    {
        print("Game over!");
    }

    private void Update() {
        if (_gameStarted){
            _gameTimer += Time.deltaTime;
            // Update game timer
            int minutes = Mathf.FloorToInt(_gameTimer / 60);
            int seconds = Mathf.FloorToInt(_gameTimer % 60);
            _timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    private bool _gameStarted = false;
    private float _gameTimer = 0f;
}
