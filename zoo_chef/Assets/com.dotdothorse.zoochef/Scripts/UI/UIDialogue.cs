using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class UIDialogue : MonoBehaviour
    {
        [SerializeField] private GameObject _dimmed;
        [SerializeField] private TextMeshProUGUI _textBox;

        public void HideEntire()
        {
            _dimmed.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void HideDimmed()
        {
            _dimmed.gameObject.SetActive(false);
        }
        
        public void HideChat()
        {
            gameObject.SetActive(false);

        }

        public void RevealEntire()
        {
            _dimmed.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        public void SetText(string sentence)
        {

            StartCoroutine(AnimateText(sentence));
        }

        private IEnumerator AnimateText(string sentence)
        {
            _textBox.text = "";
            foreach (char character in sentence.ToCharArray())
            {
                _textBox.text += character;
                yield return null;
            }
        }
    }
}