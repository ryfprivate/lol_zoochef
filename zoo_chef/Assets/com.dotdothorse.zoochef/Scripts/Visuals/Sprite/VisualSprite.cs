using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class VisualSprite : MonoBehaviour
    {
        private RectTransform rect;
        private Vector2 startSize;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            startSize = rect.sizeDelta;
            rect.sizeDelta = Vector2.zero;
        }

        private void OnDisable()
        {

        }

        public void Reveal()
        {
            rect
                .DOSizeDelta(startSize, 0.5f)
                .SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            rect
                .DOSizeDelta(Vector2.zero, 0.5f)
                .SetEase(Ease.OutBack);
        }
    }
}