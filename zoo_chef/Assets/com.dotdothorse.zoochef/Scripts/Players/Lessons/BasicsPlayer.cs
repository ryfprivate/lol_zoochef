using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace com.dotdothorse.zoochef
{
    public class BasicsPlayer : MonoBehaviour
    {
        [SerializeField] private List<DialogueSequenceSO> _sequencesToPlay;

        [Header("Game objects")]
        [SerializeField] private Button _continueButton;
        [SerializeField] private BasicsActions _actions;

        [Header("Listening and broadcasting to")]
        [SerializeField] private GameplayEventChannelSO _gameplayChannel = default;
        [SerializeField] private DialogueEventChannelSO _dialogueChannel = default;

        private void OnEnable()
        {
            _gameplayChannel.OnLevelStart += EnqueueAllSequences;
            _dialogueChannel.OnQueueReady += StartLevel;
        }

        private void OnDisable()
        {
            _gameplayChannel.OnLevelStart -= EnqueueAllSequences;
            _dialogueChannel.OnQueueReady -= StartLevel;
        }

        private void EnqueueAllSequences()
        {
            if (_sequencesToPlay != null)
            {
                _dialogueChannel.RequestFillUp(_sequencesToPlay);
            }
        }

        private void StartLevel()
        {
            StartCoroutine(MainSequence());
        }

        private IEnumerator MainSequence()
        {
            // Dialogue: Animal cafe introduction
            bool next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone += () => next = true;
            EnableButton(() => _dialogueChannel.RequestNextLine());

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Wholes
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            // Visuals: Reveal the wholes
            _actions.RevealFruitsOneByOne(() => EnableButton(() => _dialogueChannel.RequestNextLine()));

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Fractions
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            // Visuals: Move pizza
            _actions.PizzaToMiddle(() => EnableButton(() => _dialogueChannel.RequestNextLine()));

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Explain Numerator
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            // Visuals: Highlight numerator
            _actions.HighlightNumerator(() => EnableButton(() => _dialogueChannel.RequestNextLine()));

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Try out Numerator
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            // Visual: Enlarge fraction
            _actions.EnlargeNumeratorLabel();
            EnableButton(() => _dialogueChannel.RequestNextLine());

            while (!next)
                yield return null;
            DisableButton();

            // Close dialogue chat
            next = false;
            _dialogueChannel.RequestHideChat();

            // Visual: Reveal buttons
            _actions.RevealButtons1(() => {
                _dialogueChannel.RequestHideDimmed();
                EnableButton(() => next = true);
            });

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Explain Operators
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;
            
            // Visual: Twin Pizzas
            _actions.HideArrows1();
            _actions.RevealTwinPizzas(() => EnableButton(() => _dialogueChannel.RequestNextLine()));

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Greater than
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            _actions.ExampleGreaterThan();
            EnableButton(() => _dialogueChannel.RequestNextLine());

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Less than
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            _actions.ExampleLessThan();
            EnableButton(() => _dialogueChannel.RequestNextLine());

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Equals
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            _actions.ExampleEquals();
            EnableButton(() => _dialogueChannel.RequestNextLine());

            while (!next)
                yield return null;
            DisableButton();

            // Close dialogue chat
            next = false;
            _dialogueChannel.RequestHideChat();

            // Visual: Reveal buttons
            _actions.RevealButtons2(() => {
                _dialogueChannel.RequestHideDimmed();
                EnableButton(() => next = true);
            });

            while (!next)
                yield return null;
            DisableButton();

            // Dialogue: Ding dong
            next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone -= () => next = true;

            _actions.HideArrows2();
            EnableButton(() => _dialogueChannel.RequestNextLine());

            while (!next)
                yield return null;
            DisableButton();

            _dialogueChannel.RequestHide();
            _dialogueChannel.RequestReset();

            _actions.HideRemaining(() => _gameplayChannel.LevelFinished());
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