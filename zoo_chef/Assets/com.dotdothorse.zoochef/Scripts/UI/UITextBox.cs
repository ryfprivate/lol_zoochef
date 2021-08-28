using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
 
namespace com.dotdothorse.zoochef
{
    public class UITextBox : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> _textBoxes;

        private List<string> _textValues;
        private float typeSpeed = 0.05f;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Reveal(List<string> values)
        {
            _textValues = values;
            StartCoroutine(TypeText());
        }

        public void Hide(UnityAction action)
        {
            StartCoroutine(UntypeText(action));
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
                    yield return new WaitForSeconds(typeSpeed);
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
                    yield return new WaitForSeconds(typeSpeed);
                }
            }

            gameObject.SetActive(false);
            action();
        }
    }
}