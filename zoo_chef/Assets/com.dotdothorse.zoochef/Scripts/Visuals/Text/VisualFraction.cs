using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
namespace com.dotdothorse.zoochef
{
    public class VisualFraction : VisualLabel
    {
        [SerializeField] private TextMeshProUGUI _numerator;
        [SerializeField] private TextMeshProUGUI _denominator;

        public int numerator;
        public int denominator;

        public void SetValues(int num, int den)
        {
            numerator = num;
            denominator = den;

            Hide();
            _numerator.text = numerator.ToString();
            _denominator.text = denominator.ToString();
            Reveal();
        }

    }
}