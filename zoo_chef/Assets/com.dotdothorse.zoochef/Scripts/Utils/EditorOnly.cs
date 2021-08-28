using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    public class EditorOnly : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Awake()
        {
            gameObject.SetActive(false);
        }
#endif
    }
}