using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class ClickManager : MonoBehaviour
    {
        [SerializeField] private LayerMask clickLayers;

        public void SetLastInteractor(ClickInteractor interactor)
        {
            _lastInteractorClicked = interactor;
        }
        
        private void OnClick(){
            bool clicked = false;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint((Vector3) _pointerPosition);
            Vector2 mousePos2D = (Vector2) worldPos;

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, clickLayers);
            if (hit.collider != null) {
                if (hit.collider.TryGetComponent(out ClickInteractor clickInteractor)){
                    if (_lastInteractorClicked != null && _lastInteractorClicked != clickInteractor)
                        _lastInteractorClicked.Release();
                    clickInteractor.Click();
                    clicked = true;
                    _lastInteractorClicked = clickInteractor;
                }
            }
            if (!clicked && _lastInteractorClicked != null)
                _lastInteractorClicked.Release();
        }

        private void OnPointerPosition(InputValue value)
        {
            _pointerPosition = value.Get<Vector2>();
        }

        private Vector2 _pointerPosition;
        private ClickInteractor _lastInteractorClicked;
    }
}