using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Objects.Player;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Animator _gameEndScreen;

    [Header("Events")]
    [SerializeField] private GameEventChannelSO _gameEventChannel; 

    private void OnEnable() {
        _gameEventChannel.OnGameStarted += OnGameStarted;
        _gameEventChannel.OnGameWon += OnGameWon;
        _gameEventChannel.OnGameLost += OnGameLost;
    }

    private void OnDisable()
    {
        _gameEventChannel.OnGameStarted -= OnGameStarted;
        _gameEventChannel.OnGameWon -= OnGameWon;
        _gameEventChannel.OnGameLost -= OnGameLost;
    }

    private void OnGameStarted()
    {
        _gameStarted = true;
        _gameTimer = _maxGameTime;
    }

    private void OnGameWon()
    {
        _gameStarted = false;
        //_gameEndScreen.gameObject.SetActive(true);
        _gameEndScreen.Play("GameEnd");
        Instantiate(_confettiParticles, _player.transform.position, Quaternion.identity);
    }

    private void OnGameLost()
    {
        _gameStarted = false;
        //_gameEndScreen.gameObject.SetActive(true);
        _gameEndScreen.Play("GameEnd");
    }

    private void Update() {
        if (_gameStarted){
            _gameTimer -= Time.deltaTime;
            // Update game timer
            int minutes = Mathf.FloorToInt(_gameTimer / 60);
            int seconds = Mathf.FloorToInt(_gameTimer % 60);
            if (seconds >= 0)
                _timerText.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";

            if (_gameTimer <= 0f)
                _gameEventChannel.Lose();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private bool _gameStarted = false;
    private float _gameTimer = 0f;
}
