using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class VisualTextBox : MonoBehaviour
    {
        public enum Animation {
            Type,
            Grow
        }

        [SerializeField] private Animation _animationType;
        [SerializeField] private List<TextMeshProUGUI> _textBoxes;

        private List<string> _textValues;
        public float timePerChar = 0.1f;

        private void Awake()
        {
            foreach (TextMeshProUGUI textbox in _textBoxes)
            {
                textbox.text = "";
            }

            if (_animationType == Animation.Grow)
            {
                transform.localScale = Vector3.zero;
            }
        }

        public void Reveal(List<string> values)
        {
            _textValues = values;

            switch (_animationType)
            {
                case Animation.Type:
                    StartCoroutine(TypeText());
                    break;
                case Animation.Grow:
                    for (int i = 0; i < values.Count; i++)
                    {
                        _textBoxes[i].text = values[i];
                    }
                    transform
                        .DOScale(Vector3.one, 0.5f)
                        .SetEase(Ease.OutBack);
                    break;
            }
        }

        public void Hide(UnityAction action)
        {
            switch (_animationType)
            {
                case Animation.Type:
                    StartCoroutine(UntypeText(action));
                    break;
                case Animation.Grow:
                    transform
                        .DOScale(Vector3.zero, 0.5f);
                    break;
            }
        }

        private IEnumerator TypeText()
        {
            int len = _textBoxes.Count;
            for (int i = 0; i < len; i++)
            {
                string text = _textValues[i];
                foreach (char character in text)
                {
                    _textBoxes[i].text += character;
                    yield return new WaitForSeconds(timePerChar*2);
                }
            }
        }

        private IEnumerator UntypeText(UnityAction action)
        {
            int len = _textBoxes.Count;
            for (int i = len-1; i >= 0; i--)
            {
                string text = _textValues[i];
                foreach (char character in text)
                {
                    text = text.Substring(0, text.Length - 1);
                    _textBoxes[i].text = text;
                    yield return new WaitForSeconds(timePerChar);
                }
            }

            gameObject.SetActive(false);
            action();
        }
    }
}