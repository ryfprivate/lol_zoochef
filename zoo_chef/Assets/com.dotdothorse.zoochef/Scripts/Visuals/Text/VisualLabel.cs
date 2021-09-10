using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class VisualLabel : MonoBehaviour
    {
        private Vector3 scale;
        public RectTransform rect;

        public virtual void Awake()
        {
            rect = GetComponent<RectTransform>();
            scale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        public void Reveal()
        {
            transform
                .DOScale(scale, 1f)
                .SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            transform
                .DOScale(Vector3.zero, 0.5f);
        }
    }
}