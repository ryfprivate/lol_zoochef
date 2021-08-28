using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Sequence")]
    public class DialogueSequenceSO : ScriptableObject
    {
        public Sprite sprite;
        [TextArea(3, 10)]
        public List<string> sentences;
    }
}