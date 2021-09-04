using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;
 
namespace com.dotdothorse.zoochef
{
    public class LessonBasicsActions : MonoBehaviour
    {
        [Header("Wholes")]
        [SerializeField] private VisualSpriteWithLabel _pizza;
        [SerializeField] private VisualSpriteWithLabel _cake;
        [SerializeField] private VisualSpriteWithLabel _sandwich;

        [Header("Fractions")]
        [SerializeField] private VisualFractionedSprite _fractionedPizza1;
        [SerializeField] private VisualLabel _fractionLabel1;
        [SerializeField] private VisualLabel _fractionLabel2;

        [Header("Numerator")]
        [SerializeField] private VisualLabel _numeratorLabel1;
        [SerializeField] private VisualTextBox _numeratorText;

        [Header("Change Numerator 1")]
        [SerializeField] private TextMeshProUGUI _numeratorTextBox1;
        [SerializeField] private ButtonChangeNumerator _leftButton1;
        [SerializeField] private ButtonChangeNumerator _rightButton1;

        [Header("Operators")]
        [SerializeField] private VisualFractionedSprite _fractionedPizza2;
        [SerializeField] private VisualLabel _numeratorLabel2;

        [Header("Change Operators")]
        [SerializeField] private VisualTextBox _operatorText;
        [SerializeField] private TextMeshProUGUI _numeratorTextBox2;

        [Header("Change Numerator 2")]
        [SerializeField] private RectTransform _leftButtons;
        [SerializeField] private ButtonChangeNumerator _leftButton2;
        [SerializeField] private ButtonChangeNumerator _rightButton2;

        public void HideRemaining(UnityAction action)
        {
            StartCoroutine(CHideRemaining(action));
        }

        private IEnumerator CHideRemaining(UnityAction action)
        {
            _fractionedPizza1.Hide();
            _numeratorLabel1.Hide();
            _operatorText.Hide(() => { });
            _fractionedPizza2.Hide();
            _numeratorLabel2.Hide();
            yield return new WaitForSeconds(1f);

            action();
        }

        public void HideArrows2()
        {
            _leftButton1.Deactivate();
            _rightButton1.Deactivate();
            _leftButton2.Deactivate();
            _rightButton2.Deactivate();
        }

        public void RevealButtons2(UnityAction action)
        {
            StartCoroutine(CRevealButtons2(action));
        }

        private IEnumerator CRevealButtons2(UnityAction action)
        {
            _leftButtons.anchoredPosition = new Vector2(-400, 0);
            // Activate Buttons
            _leftButton1.Activate(() =>
            {
                _fractionedPizza1.TickNumerator(false);
                _numeratorTextBox1.text = _fractionedPizza1.numerator.ToString();

                CheckButtons1Valid();
                CheckOperator();
            });
            _rightButton1.Activate(() =>
            {
                _fractionedPizza1.TickNumerator(true);
                _numeratorTextBox1.text = _fractionedPizza1.numerator.ToString();

                CheckButtons1Valid();
                CheckOperator();
            });

            _leftButton2.Activate(() =>
            {
                _fractionedPizza2.TickNumerator(false);
                _numeratorTextBox2.text = _fractionedPizza2.numerator.ToString();

                CheckButtons2Valid();
                CheckOperator();
            });
            _rightButton2.Activate(() =>
            {
                _fractionedPizza2.TickNumerator(true);
                _numeratorTextBox2.text = _fractionedPizza2.numerator.ToString();

                CheckButtons2Valid();
                CheckOperator();
            });
            yield return new WaitForSeconds(1f);

            action();
        }

        private void CheckButtons2Valid()
        {
            if (_fractionedPizza2.numerator == 0)
            {
                _leftButton2.Hide();
            }
            else
            {
                _leftButton2.Reveal();
            }

            if (_fractionedPizza2.numerator == _fractionedPizza2.denominator)
            {
                _rightButton2.Hide();
            }
            else
            {
                _rightButton2.Reveal();
            }
        }

        private void CheckOperator()
        {
            List<string> values = new List<string>();
            if (_fractionedPizza1.numerator == _fractionedPizza2.numerator)
            {
                values.Add("=");
            }
            if (_fractionedPizza1.numerator > _fractionedPizza2.numerator)
            {
                values.Add(">");
            }
            if (_fractionedPizza1.numerator < _fractionedPizza2.numerator)
            {
                values.Add("<");
            }

            _operatorText.Reveal(values);
        }

        public void ExampleEquals()
        {
            List<string> values = new List<string>();
            values.Add("=");
            _operatorText.Reveal(values);

            _fractionedPizza1.ChangeValue(2, 4);
            _numeratorTextBox1.text = _fractionedPizza1.numerator.ToString();
            _fractionedPizza2.ChangeValue(2, 4);
            _numeratorTextBox2.text = _fractionedPizza2.numerator.ToString();
        }

        public void ExampleLessThan()
        {
            List<string> values = new List<string>();
            values.Add("<");
            _operatorText.Reveal(values);

            _fractionedPizza1.ChangeValue(1, 4);
            _numeratorTextBox1.text = _fractionedPizza1.numerator.ToString();
            _fractionedPizza2.ChangeValue(3, 4);
            _numeratorTextBox2.text = _fractionedPizza2.numerator.ToString();
        }

        public void ExampleGreaterThan()
        {
            List<string> values = new List<string>();
            values.Add(">");
            _operatorText.Reveal(values);

            _fractionedPizza1.ChangeValue(2, 4);
            _numeratorTextBox1.text = _fractionedPizza1.numerator.ToString();
            _fractionedPizza2.ChangeValue(1, 4);
            _numeratorTextBox2.text = _fractionedPizza2.numerator.ToString();
        }

        public void RevealTwinPizzas(UnityAction action)
        {
            StartCoroutine(CRevealTwinPizzas(action));
        }

        private IEnumerator CRevealTwinPizzas(UnityAction action)
        {
            _fractionedPizza1.rect
                .DOAnchorPos(new Vector2(-360f, 100f), 1f);
            _fractionedPizza1.gameObject.transform
                .DOScale(new Vector3(2, 2, 2), 1f);

            _numeratorLabel1.rect
                .DOAnchorPos(new Vector2(-150f, 150f), 1f);

            _fractionedPizza2.gameObject.SetActive(true);
            _fractionedPizza2.Reveal();
            _numeratorLabel2.Reveal();

            yield return new WaitForSeconds(1f);

            action();
        }

        public void HideArrows1()
        {
            _leftButton1.Deactivate();
            _rightButton1.Deactivate();
        }

        public void RevealButtons1(UnityAction action)
        {
            StartCoroutine(CRevealButtons1(action));
        }

        private IEnumerator CRevealButtons1(UnityAction action)
        {
            // Activate Buttons
            _leftButton1.Activate(() =>
            {
                _fractionedPizza1.TickNumerator(false);
                _numeratorTextBox1.text = _fractionedPizza1.numerator.ToString();

                CheckButtons1Valid();
            });
            _rightButton1.Activate(() =>
            {
                _fractionedPizza1.TickNumerator(true);
                _numeratorTextBox1.text = _fractionedPizza1.numerator.ToString();

                CheckButtons1Valid();
            });
            yield return new WaitForSeconds(1f);

            action();
        }

        private void CheckButtons1Valid()
        {
            if (_fractionedPizza1.numerator == 0)
            {
                _leftButton1.Hide();
            }
            else
            {
                _leftButton1.Reveal();
            }

            if (_fractionedPizza1.numerator == _fractionedPizza1.denominator)
            {
                _rightButton1.Hide();
            }
            else
            {
                _rightButton1.Reveal();
            }
        }

        public void EnlargeNumeratorLabel()
        {
            _numeratorText.Hide(() => { });
            _numeratorLabel1.rect
                .DOAnchorPos(new Vector2(250f, 150f), 1f);
            _numeratorLabel1.gameObject.transform
                .DOScale(new Vector3(2, 2, 2), 1f);
        }

        public void HighlightNumerator(UnityAction action)
        {
            StartCoroutine(CHighlightNumerator(action));
        }

        private IEnumerator CHighlightNumerator(UnityAction action)
        {
            _fractionLabel1.Hide();
            _fractionLabel2.Hide();

            _fractionedPizza1.rect
                .DOAnchorPos(new Vector2(-100f, 100f), 1f);
            _fractionedPizza1.gameObject.transform
                .DOScale(new Vector3(2.5f, 2.5f, 2.5f), 1f);
            yield return new WaitForSeconds(1f);

            _fractionedPizza1.ChangeValue(1, 4);
            _numeratorLabel1.Reveal();
            List<string> values = new List<string>();
            values.Add("- Numerator");
            _numeratorText.Reveal(values);

            action();
        }

        public void PizzaToMiddle(UnityAction action)
        {
            StartCoroutine(CPizzaToMiddle(action));
        }

        private IEnumerator CPizzaToMiddle(UnityAction action)
        {
            float duration = 0.5f;

            _sandwich.Hide();
            _cake.Hide();
            yield return new WaitForSeconds(duration);
            _pizza.Hide();

            _fractionedPizza1.rect
                .DOAnchorPos(new Vector2(0, 100f), 1f);
            _fractionedPizza1.gameObject.transform
                .DOScale(new Vector3(2, 2, 2), 1f);
            yield return new WaitForSeconds(1f);

            _fractionedPizza1.Reveal();
            RevealLabels();

            yield return new WaitForSeconds(1f);
            action();
        }

        private void RevealLabels()
        {
            _fractionLabel1.Reveal();
            _fractionLabel2.Reveal();
        }

        public void RevealFruitsOneByOne(UnityAction action)
        {
            StartCoroutine(CRevealFruitsOneByOne(action));
        }

        private IEnumerator CRevealFruitsOneByOne(UnityAction action)
        {
            float duration = 0.5f;

            _pizza.Reveal();
            yield return new WaitForSeconds(duration);
            _fractionedPizza1.gameObject.SetActive(true);

            _cake.Reveal();
            yield return new WaitForSeconds(duration);
            _sandwich.Reveal();
            yield return new WaitForSeconds(duration);
            action();
        }

    }
}