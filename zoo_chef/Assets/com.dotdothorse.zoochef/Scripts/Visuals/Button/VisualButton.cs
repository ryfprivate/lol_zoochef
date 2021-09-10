using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
 
namespace com.dotdothorse.zoochef
{
    public class VisualButton : VisualLabel
    {
        [SerializeField] private GameObject _focus;
        private Button button;

        public override void Awake()
        {
            base.Awake();
            button = GetComponent<Button>();
        }

        public void Select()
        {
            _focus.SetActive(true);
        }

        public void Deselect()
        {
            _focus.SetActive(false);
        }

        public void Register(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        public void Deregister()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}