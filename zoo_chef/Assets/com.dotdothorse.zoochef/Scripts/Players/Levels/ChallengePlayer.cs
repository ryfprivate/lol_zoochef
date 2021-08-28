using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace com.dotdothorse.zoochef
{
    public class ChallengePlayer : MonoBehaviour
    {
        [SerializeField] private UIChallenge _uiChallenge;

        [SerializeField] private Camera _cam;
        [SerializeField] private List<Transform> _backgrounds;

        public bool start = false;

        private void Start()
        {
            _uiChallenge.gameObject.SetActive(false);
            StartCoroutine(MainSequence());
        }

        private IEnumerator MainSequence()
        {
            while (!start)
                yield return null;

            float entranceDuration = 4f;
            Entrance(entranceDuration);
            yield return new WaitForSeconds(entranceDuration);

            _uiChallenge.gameObject.SetActive(true);
        }

        private void Entrance(float duration)
        {
            _cam.gameObject.transform
                .DOMoveX(transform.position.x - 0.3f, 4f)
                .SetEase(Ease.Linear);

            float moveAmount = 0.8f;

            foreach (Transform bg in _backgrounds)
            {
                bg
                    .DOMoveX(bg.position.x + moveAmount, 4f)
                    .SetEase(Ease.Linear);
                moveAmount -= 0.2f;
            }
        }
    }
}