using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class VisualFood : MonoBehaviour
    {
        private RectTransform rect;
        private Vector2 startSize;
        private Vector2 startPosition;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            startSize = rect.sizeDelta;
            startPosition = rect.anchoredPosition;
            rect.sizeDelta = Vector2.zero;
        }

        public void Feed(float duration)
        {
            StartCoroutine(CFeed(duration));
        }

        public IEnumerator CFeed(float duration)
        {
            rect.DOSizeDelta(Vector2.zero, duration);
            yield return new WaitForSeconds(duration);

            rect.anchoredPosition = startPosition;
        }

        public void MoveTo(Vector2 position)
        {
            StartCoroutine(CMoveTo(position));
        }

        public IEnumerator CMoveTo(Vector2 position)
        {
            float duration = 1;
            rect
                .DOSizeDelta(startSize, duration);
            rect
                .DOAnchorPos(position, duration);

            yield return new WaitForSeconds(1f);

        }
    }
}