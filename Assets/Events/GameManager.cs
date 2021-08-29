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
    [SerializeField] private Image _fadeImage;

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
        StartCoroutine(FadeOutScreen(1.0f, 3.0f));
        Instantiate(_confettiParticles, _player.transform.position, Quaternion.identity);
    }

    private void OnGameLost()
    {
        StartCoroutine(FadeOutScreen(1.0f));
    }

    private void Update() {
        if (_gameStarted){
            _gameTimer -= Time.deltaTime;
            // Update game timer
            int minutes = Mathf.FloorToInt(_gameTimer / 60);
            int seconds = Mathf.FloorToInt(_gameTimer % 60);
            if (seconds >= 0)
                _timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

            if (_gameTimer <= 0f)
                _gameEventChannel.Lose();
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private IEnumerator FadeInScreen(float duration)
    {
        float time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            Color col = _fadeImage.color;
            col.a = (1.0f - Mathf.Clamp01(time / duration));
            _fadeImage.color = col;
            yield return null;
        }
    }

    private IEnumerator FadeOutScreen(float duration, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        float time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            Color col = _fadeImage.color;
            col.a = Mathf.Clamp01(time / duration);
            _fadeImage.color = col;
            yield return null;
        }
        RestartScene();
    }

    private bool _gameStarted = false;
    private float _gameTimer = 0f;
}
