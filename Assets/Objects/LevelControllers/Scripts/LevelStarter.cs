using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Objects.LevelControllers.Scripts
{
    public class LevelStarter : MonoBehaviour
    {
        [Header("Dialog")]
        [SerializeField] private OnBoardingDialog _onBoardingDialog;
        [SerializeField] private float _afterDialogDelay = 4f;

        [Header("Movement")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Player.Player _player;
        [SerializeField] private BotMovement _botMovement;
        [SerializeField] private Transform _botEndPosition;
        [SerializeField] private Transform _playerEndPosition;
        [SerializeField] private Vector2 _botPositionOffset = Vector2.left*3;

        [Header("Required To enable on Gameplay")] [SerializeField]
        private GameObject _gameScreen;
        
        public void StartBeforeLevelDialog()
        {
            _playerMovement.Stop();
            _playerMovement.BlockPlayerInput();
            _botMovement.MoveAt((Vector2)_playerMovement.transform.position+_botPositionOffset);
            _onBoardingDialog.StartDialogDelay(EndOnboarding);
        }

        private void EndOnboarding()
        {
            _botMovement.MoveAt(_botEndPosition.position);
            _playerMovement.MoveAt(_playerEndPosition.position);
            StartCoroutine(WaitForDelay());
        }

        private void StartGameplay()
        {
            _playerMovement.UnblockPlayerInput();
            _player.IsChangingArea = false;
            _gameScreen.gameObject.SetActive(true);
        }

        private IEnumerator WaitForDelay()
        {
            yield return new WaitForSeconds(_afterDialogDelay);
            StartGameplay();
        }
    }
}