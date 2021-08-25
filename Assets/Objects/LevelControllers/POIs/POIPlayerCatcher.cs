using System;
using UnityEngine;

namespace Objects.LevelControllers.POIs
{
    public class POIPlayerCatcher : MonoBehaviour
    {
        [SerializeField] private Vector2 _additionalMove;
        public event Action PlayerWalkedIn;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out Player.Player _player)) return;
            if (_player.IsChangingArea) return;
            _player.AdditionalMove(_additionalMove);
            _player.IsChangingArea = true;
            _startedChangingFromHere = true;
            PlayerWalkedIn?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out Player.Player _player)) return;
            if (_player.IsChangingArea && !_startedChangingFromHere) _player.IsChangingArea = false;
            if (_startedChangingFromHere) _startedChangingFromHere = false;
        }

        private bool _startedChangingFromHere;
    }
}