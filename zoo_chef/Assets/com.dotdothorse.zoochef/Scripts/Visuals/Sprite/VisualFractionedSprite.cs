using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class VisualFractionedSprite : MonoBehaviour
    {
        [SerializeField] private Image _filledImage;
        [SerializeField] private GameObject _line;
        public RectTransform rect;

        [Header("Fraction")]
        public int numerator;
        public int denominator;

        private List<GameObject> lineInstances;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            lineInstances = new List<GameObject>();
            CreateLines();
            gameObject.SetActive(false);
        }

        public void TickNumerator(bool increment)
        {
            if (increment)
            {
                if (numerator == denominator) return;
                numerator++;
            } else
            {
                if (numerator == 0) return;
                numerator--;
            }
                
            CreateLines();
            Reveal();
        }

        public void ChangeValue(int newNumerator, int newDenominator)
        {
            numerator = newNumerator;
            denominator = newDenominator;
            CreateLines();
            Reveal();
        }

        public void Reveal()
        {
            if (denominator == 0) return;

            foreach (GameObject line in lineInstances)
            {
                line.SetActive(true);
            }

            float amount = numerator / (float)denominator;

            _filledImage.fillAmount = amount;
        }

        public void Hide()
        {
            transform
                .DOScale(Vector3.zero, 1f);
        }

        private void CreateLines()
        {
            GetRidOfLines();
            if (denominator == 0) return;

            // Spawn lines
            float angle = 360f / denominator;
            for (int i = 0; i < denominator; i++)
            {
                GameObject line = Instantiate(_line);
                line.transform.SetParent(transform);
                RectTransform rect = line.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(0, 50, 0);
                rect.localRotation = Quaternion.Euler(0, 0, angle * i);
                rect.sizeDelta = new Vector2(25, 50);
                rect.localScale = Vector3.one;
                line.SetActive(false);

                lineInstances.Add(line);
            }
        }

        private void GetRidOfLines()
        {
            foreach (GameObject line in lineInstances)
            {
                Destroy(line);
            }
            lineInstances = new List<GameObject>();
        }

    }
}