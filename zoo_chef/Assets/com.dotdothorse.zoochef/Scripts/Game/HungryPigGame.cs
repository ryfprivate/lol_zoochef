using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
 
namespace com.dotdothorse.zoochef
{
    public class HungryPigGame : MonoBehaviour
    {
        public enum Operator
        {
            lesserThan,
            equals,
            greaterThan
        }

        [SerializeField] private VisualLabel _challengeTitle;

        [Header("Fraction labels")]
        [SerializeField] private VisualFraction _left;
        [SerializeField] private VisualFraction _main;

        [Header("Operator")]
        [SerializeField] private VisualOperator _operator;

        [Header("Operator options")]
        [SerializeField] private VisualButton _lessButton;
        [SerializeField] private VisualButton _equalsButton;
        [SerializeField] private VisualButton _greaterButton;

        [Header("Food")]
        [SerializeField] private VisualFood _food;
        [SerializeField] private Slider _foodSlider;

        [Header("Animals")]
        [SerializeField] private AnimalController _animalController;

        [Header("Broadcasting to")]
        [SerializeField] private GameplayEventChannelSO _gameplayChannel = default;

        private int denominator;

        private int numQuestions1;
        private int questionCount;

        private void Awake()
        {
            _foodSlider.transform.localScale = Vector3.zero;
        }

        public void RevealButtons()
        {
            _lessButton.Reveal();
            _equalsButton.Reveal();
            _greaterButton.Reveal();
        }

        public void StartGame()
        {
            numQuestions1 = 5;
            questionCount = 0;

            _foodSlider.value = 0;
            _foodSlider.transform.localScale = Vector3.one;

            StartCoroutine(CStartGame());
        }

        public IEnumerator CStartGame()
        {
            _challengeTitle.Reveal();

            // Register button listeners
            EnableButtons();
            
            yield return new WaitForSeconds(1);

            denominator = Random.Range(6, 10);
            int value = Random.Range(0, 2);
            _left.SetValues(value, denominator);
            _main.SetValues(1, denominator);

            _operator.SetText("?");
            _operator.Reveal();

            while (questionCount < numQuestions1)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2);
            _foodSlider.transform
                .DOScale(Vector3.zero, 1f);
            yield return new WaitForSeconds(1);
            _gameplayChannel.ChallengeCompleted();
        }

        private void EnableButtons()
        {
            _lessButton.Register(() => CheckAnswer(Operator.lesserThan));
            _equalsButton.Register(() => CheckAnswer(Operator.equals));
            _greaterButton.Register(() => CheckAnswer(Operator.greaterThan));
        }

        private void DisableButtons()
        {
            _lessButton.Deregister();
            _equalsButton.Deregister();
            _greaterButton.Deregister();
        }

        private void CheckAnswer(Operator op)
        {
            DisableButtons();
            if (op == Operator.lesserThan)
            {
                if (_left.numerator < _main.numerator)
                {
                    StartCoroutine(CorrectAnswer("<"));
                } else
                {
                    StartCoroutine(IncorrectAnswer("<"));
                }
            }

            if (op == Operator.equals)
            {
                if (_left.numerator == _main.numerator)
                {
                    StartCoroutine(CorrectAnswer("="));
                } else
                {
                    StartCoroutine(IncorrectAnswer("="));
                }
            }

            if (op == Operator.greaterThan)
            {
                if (_left.numerator > _main.numerator)
                {
                    StartCoroutine(CorrectAnswer(">"));
                    StartCoroutine(NextMeal(_left.numerator));
                } else
                {
                    StartCoroutine(IncorrectAnswer(">"));
                }
            }
        }

        private IEnumerator NextMeal(int newValue)
        {
            questionCount++;
            _food.MoveTo(new Vector2(405, -220));
            yield return new WaitForSeconds(1);

            float duration = 2;
            float sliderValue = questionCount / (float)numQuestions1;
            _foodSlider
                .DOValue(sliderValue, duration);
            _food.Feed(duration);
            _animalController.StartEating();

            _main.SetValues(newValue, denominator);

            yield return new WaitForSeconds(duration);

            _animalController.StartIdling();
        }

        private IEnumerator CorrectAnswer(string op)
        {
            _operator.Correct(op);
            yield return new WaitForSeconds(1);

            ChangeLeftFraction();
        }

        private IEnumerator IncorrectAnswer(string op)
        {
            _operator.Incorrect(op);
            yield return new WaitForSeconds(1);

            ChangeLeftFraction();
        }

        private void ChangeLeftFraction()
        {
            int value = Random.Range(_main.numerator - 1, _main.numerator + 3);
            _left.SetValues(value, denominator);
            _operator.SetText("?");
            EnableButtons();
        }
    }
}