using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace com.dotdothorse.zoochef
{
    public class ChallengePlayer : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private ChallengeDataSO _data;

        [Header("UI")]
        [SerializeField] private UIChallenge _uiChallenge;
        [SerializeField] private UIBackgroundScreen _uiBackgroundScreen;

        [Header("GameObjects")]
        [SerializeField] private Camera _cam;
        [SerializeField] private List<Transform> _backgrounds;
        [SerializeField] private AnimalController _animalController;

        [Header("Listening to")]
        [SerializeField] private GameplayEventChannelSO _gameplayChannel = default;

        private void OnEnable()
        {
            _gameplayChannel.OnLevelStart += StartChallenge;
        }

        private void OnDisable()
        {
            _gameplayChannel.OnLevelStart -= StartChallenge;
        }

        private void StartChallenge()
        {
            StartCoroutine(MainSequence());
        }

        private IEnumerator MainSequence()
        {
            float panDuration = 2f;
            Pan(panDuration);
            yield return new WaitForSeconds(panDuration);

            float entranceDuration = 2f;
            _animalController.StartWalking();
            Entrance(entranceDuration);
            yield return new WaitForSeconds(entranceDuration);
            _animalController.StopWalking();

            _uiBackgroundScreen.RevealCustomerScreen();
            yield return new WaitForSeconds(1f);
            _uiChallenge.RevealCustomerText(_data);

            yield return new WaitForSeconds(5f);
            _uiChallenge.HideCustomerText(
                () => _uiBackgroundScreen.HideCustomerScreen()
                );
        }

        private void Pan(float duration)
        {
            _cam.gameObject.transform
                .DOMoveX(transform.position.x - 0.3f, duration)
                .SetEase(Ease.Linear);

            float moveAmount = 1.2f;

            foreach (Transform bg in _backgrounds)
            {
                bg
                    .DOMoveX(bg.position.x + moveAmount, duration)
                    .SetEase(Ease.Linear);
                moveAmount -= 0.3f;
            }
        }

        private void Entrance(float duration)
        {
            GameObject animals = _animalController.gameObject;
            animals.transform
                .DOMoveX(animals.transform.position.x - 7f, duration)
                .SetEase(Ease.Linear);
        }
    }
}