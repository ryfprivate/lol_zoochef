using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace com.dotdothorse.zoochef
{
    public class VisualOperator : VisualLabel
    {
        [SerializeField] private TextMeshProUGUI _operator;

        public void SetText(string op)
        {
            _operator.text = op;
            _operator.color = Color.white;
        }

        public void Correct(string op)
        {
            _operator.text = op;
            _operator.transform
                .DOPunchScale(new Vector3(2f, 2f, 2f), 1, 0);
            _operator.color = Color.green;
        }

        public void Incorrect(string op)
        {
            _operator.text = op;
            _operator.transform
                .DOShakeScale(1);
            _operator.color = Color.red;
        }
    }
}