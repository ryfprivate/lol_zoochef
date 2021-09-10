using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class IntroductionActions : MonoBehaviour
    {
        [Header("Backgrounds")]
        [SerializeField] private Transform _background;
        [SerializeField] private UIBackgroundScreen _coverScreen;

        [Header("Animals")]
        [SerializeField] private AnimalController _animalController;

        [Header("Text")]
        [SerializeField] private VisualTextBox _entranceTextBox;

        public void EndIntroduction(UnityAction action)
        {
            StartCoroutine(CEndIntroduction(action));
        }

        public IEnumerator CEndIntroduction(UnityAction action)
        {
            bool next = false;
            _entranceTextBox.Hide(() => next = true);

            while (!next)
                yield return null;

            _coverScreen.HideCustomerScreen();
            yield return new WaitForSeconds(1);

            action();
        }

        public void CustomerRequest(string request, UnityAction action)
        {
            StartCoroutine(CCustomerRequest(request, action));
        }

        public IEnumerator CCustomerRequest(string request, UnityAction action)
        {
            bool next = false;
            _entranceTextBox.Hide(() => next = true);

            while (!next)
                yield return null;

            List<string> values = new List<string>();
            values.Add("Request:");
            values.Add(request);
            _entranceTextBox.Reveal(values);
            yield return new WaitForSeconds(1f);

            action();
        }

        public void CustomerIntroduction(string characterName, UnityAction action)
        {
            StartCoroutine(CCustomerIntroduction(characterName, action));
        }

        public IEnumerator CCustomerIntroduction(string characterName, UnityAction action)
        {
            float duration = 1;
            _background
                .DOMoveX(-0.29f, duration)
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);

            duration = 2;
            _animalController.StartWalking();
            GameObject animals = _animalController.gameObject;
            animals.transform
                .DOMoveX(animals.transform.position.x - 5.25f, duration)
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);
            _animalController.StartIdling();

            duration = 1;
            _coverScreen.RevealCustomerScreen(duration);
            yield return new WaitForSeconds(duration);

            List<string> values = new List<string>();
            values.Add("New Customer:");
            values.Add(characterName);
            _entranceTextBox.Reveal(values);

            action();
        }
    }
}