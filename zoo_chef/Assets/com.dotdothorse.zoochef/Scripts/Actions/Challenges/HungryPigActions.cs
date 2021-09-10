using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class HungryPigActions : MonoBehaviour
    {
        [SerializeField] private VisualLabel _challengePopup;
        [SerializeField] private HungryPigGame _game;
        [SerializeField] private UIBackgroundScreen _coverScreen;

        public void ShowVictoryScreen(UnityAction action)
        {
            StartCoroutine(CShowVictoryScreen(action));
        }

        public IEnumerator CShowVictoryScreen(UnityAction action)
        {
            _challengePopup.Hide();
            yield return new WaitForSeconds(1);

            float duration = 1;
            _coverScreen.RevealVictoryScreen(duration);

            yield return new WaitForSeconds(duration);

            action();
        }

        public void RevealGame()
        {
            _game.StartGame();
        }

        public void StartChallengePopup(UnityAction action)
        {
            StartCoroutine(CStartChallengePopup(action));
        }

        public IEnumerator CStartChallengePopup(UnityAction action)
        {
            float duration = 1;
            _challengePopup.Reveal();
            yield return new WaitForSeconds(duration);

            _game.RevealButtons();
            yield return new WaitForSeconds(duration);

            action();
        }
    }
}