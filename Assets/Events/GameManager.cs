using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using EventChannels;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ParticleSystem _confettiParticles;
    [SerializeField] private TMP_Text _timerText;

    [Header("Events")]
    [SerializeField] private GameEventChannelSO _gameEventChannel; 

    private float _gameTime = 0f;
}
