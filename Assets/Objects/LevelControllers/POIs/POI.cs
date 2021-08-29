using System;
using Cinemachine;
using Objects.LevelControllers.POIs;
using UnityEngine;

namespace Objects.LevelControllers
{
    public class POI : MonoBehaviour
    {
        public string POIName;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private POIPlayerCatcher[] _playerCatchers;
        public event EventHandler PlayerSteppedIn;

        public void ResetCameraPriority()
        {
            _camera.Priority = 0;
        }

        private void OnEnable()
        {
            foreach (var catcher in _playerCatchers)
            {
                catcher.PlayerWalkedIn += OnPlayerWalkedIn;
            }
            
        }

        private void OnDisable()
        {
            foreach (var catcher in _playerCatchers)
            {
                catcher.PlayerWalkedIn -= OnPlayerWalkedIn;
            }
        }

        private void OnPlayerWalkedIn()
        {
            PlayerSteppedIn?.Invoke(this,EventArgs.Empty);
            _camera.Priority = 10;
        }
    }
}