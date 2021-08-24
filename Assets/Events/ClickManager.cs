using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Bots.Scripts;
using UnityEngine.InputSystem;

namespace Managers
{
    public class ClickManager : MonoBehaviour
    {
        [SerializeField] private LayerMask clickLayers;

        private void OnClick(){
            Vector3 worldPos = Camera.main.ScreenToWorldPoint((Vector3) _pointerPosition);
            Vector2 mousePos2D = (Vector2) worldPos;

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, clickLayers);
            if (hit.collider != null) {
                if (hit.collider.TryGetComponent(out ClickInteractor clickInteractor)){
                    clickInteractor.Click();
                    if (hit.collider.TryGetComponent(out Bot bot)){
                        if (_lastBotClicked != null && _lastBotClicked != bot)
                            _lastBotClicked.ReleaseClick();
                        _lastBotClicked = bot;
                    }
                }
            }
        }

        private void OnPointerPosition(InputValue value)
        {
            _pointerPosition = value.Get<Vector2>();
        }

        private Vector2 _pointerPosition;
        private Bot _lastBotClicked;
    }
}