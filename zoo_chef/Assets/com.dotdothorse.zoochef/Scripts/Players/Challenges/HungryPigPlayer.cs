using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace com.dotdothorse.zoochef
{
    public class HungryPigPlayer : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField] private List<DialogueSequenceSO> _sequencesToPlay;
        [Header("Data")]
        [SerializeField] private ChallengeDataSO _data;

        [Header("Game objects")]
        [SerializeField] private Button _continueButton;
        [SerializeField] private IntroductionActions _introActions;
        [SerializeField] private HungryPigActions _actions;

        [Header("Listening to")]
        [SerializeField] private GameplayEventChannelSO _gameplayChannel = default;

        [Header("Listening and broadcasting to")]
        [SerializeField] private DialogueEventChannelSO _dialogueChannel = default;

        private void OnEnable()
        {
            _gameplayChannel.OnLevelStart += EnqueueAllSequences;
            _dialogueChannel.OnQueueReady += StartChallenge;
        }

        private void OnDisable()
        {
            _gameplayChannel.OnLevelStart -= EnqueueAllSequences;
            _dialogueChannel.OnQueueReady -= StartChallenge;
        }

        private void EnqueueAllSequences()
        {
            if (_sequencesToPlay != null)
            {
                _dialogueChannel.RequestFillUp(_sequencesToPlay, false);
            }
        }

        private void StartChallenge()
        {
            StartCoroutine(MainSequence());
        }

        private IEnumerator MainSequence()
        {
            DisableButton();
            bool next = false;
            _introActions.CustomerIntroduction(_data.characterName,
                () => EnableButton(() => next = true));

            while (!next)
                yield return null;
            DisableButton();

            next = false;
            _introActions.CustomerRequest(_data.request,
                () => EnableButton(() => next = true));

            while (!next)
                yield return null;
            DisableButton();

            next = false;
            _introActions.EndIntroduction(
                () => _actions.StartChallengePopup(
                    () => {
                        _dialogueChannel.RequestStart();
                        EnableButton(() => _dialogueChannel.RequestNextLine());
                    }));
            _dialogueChannel.OnDialogueDone += () => next = true;

            while (!next)
                yield return null;
            DisableButton();

            next = false;
            _dialogueChannel.RequestHide();
            _actions.RevealGame();
            _gameplayChannel.OnChallengeCompleted += () => next = true;

            while (!next)
                yield return null;

            next = false;
            _actions.ShowVictoryScreen(
                () => EnableButton(() => next = true));

            while (!next)
                yield return null;

            _gameplayChannel.LevelFinished();
        }

        private void EnableButton(UnityAction action)
        {
            _continueButton.gameObject.SetActive(true);
            _continueButton.onClick.AddListener(action);
        }

        private void DisableButton()
        {
            _continueButton.onClick.RemoveAllListeners();
            _continueButton.gameObject.SetActive(false);
        }
    }
}