using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogs;
using TMPro;

namespace Dialogs
{
    public class DialogBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textLabel;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void Initialize(DialogSO dialog, Transform dialogPointTransform)
        {
            _dialog = dialog;
            _textLabel.text = $"<b>{dialog.fromBot.Config.BotName}</b>\n{dialog.text}";
            _targetTransform = dialogPointTransform;
            _parentRectTransform = transform.parent.GetComponent<RectTransform>();
            _thisRectTransform = GetComponent<RectTransform>();
        }

        public void Close()
        {
            _animator.Play("Hide");
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public void UpdateDialog(DialogSO dialog)
        {
            _textLabel.text = $"<b>{dialog.fromBot.Config.BotName}</b>\n{dialog.text}";
        }

        private void Update()
        {
            var canvasPosition = Camera.main.WorldToScreenPoint(_targetTransform.position);
            transform.position = canvasPosition;
            ClampToScreen(_parentRectTransform, _thisRectTransform );
        }


        private void ClampToScreen(RectTransform canvas, RectTransform obj)
        {
            var sizeDelta = canvas.sizeDelta - obj.sizeDelta;
            var objPivot = obj.pivot;
            var position = obj.anchoredPosition;
            position.x = Mathf.Clamp(position.x, -sizeDelta.x * objPivot.x, sizeDelta.x * (1 - objPivot.x));
            position.y = Mathf.Clamp(position.y, -sizeDelta.y * objPivot.y, sizeDelta.y * (1 - objPivot.y));
            obj.anchoredPosition = position;
        }


        private RectTransform _parentRectTransform;

        private RectTransform _thisRectTransform;

        private Transform _targetTransform;

        private DialogSO _dialog;

        private Animator _animator;
    }
}