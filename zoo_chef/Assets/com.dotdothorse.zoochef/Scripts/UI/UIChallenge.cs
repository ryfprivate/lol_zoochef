using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
namespace com.dotdothorse.zoochef
{
    public class UIChallenge : MonoBehaviour
    {
        [SerializeField] private UITextBox _entranceTextBox;
        [SerializeField] private GameObject _questionUI;

        public void RevealCustomerText(ChallengeDataSO data)
        {
            _entranceTextBox.gameObject.SetActive(true);

            List<string> values = new List<string>();
            values.Add("New Customer:");
            values.Add(data.characterName);
            _entranceTextBox.Reveal(values);
        }

        public void HideCustomerText(UnityAction action)
        {
            _entranceTextBox.Hide(action);
        }
    }
}