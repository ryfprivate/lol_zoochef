using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class ButtonChangeNumerator : MonoBehaviour
    {
        private Image _image;
        private Button _button;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _image.color = Color.clear;
        }

        public void Activate(UnityAction action)
        {   
            _button.onClick.AddListener(action);
            Reveal();
        }

        public void Deactivate()
        {
            _button.onClick.RemoveAllListeners();
            Hide();
        }

        public void Reveal()
        {
            _image
                .DOColor(Color.white, 0.5f);
        }

        public void Hide()
        {
            _image
                .DOColor(Color.clear, 0.5f);
        }
    }
}