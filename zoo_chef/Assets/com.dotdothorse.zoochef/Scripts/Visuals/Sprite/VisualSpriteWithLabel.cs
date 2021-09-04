using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    public class VisualSpriteWithLabel : MonoBehaviour
    {
        [TextArea]
        [SerializeField]
        private string label;

        [SerializeField] private VisualSprite _sprite;
        [SerializeField] private VisualTextBox _textBox;

        public void Reveal()
        {
            _sprite.Reveal();

            List<string> list = new List<string>();
            list.Add(label);
            _textBox.Reveal(list);
        }

        public void Hide()
        {
            _sprite.Hide();

            _textBox.Hide(() => { });
        }
    }
}