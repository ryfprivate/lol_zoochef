using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace com.dotdothorse.zoochef
{
    public class MoveSlowly : MonoBehaviour
    {
        [SerializeField] private Camera _cam;

        public bool on = false;

        private void Update()
        {
            if (on)
            {
                _cam.gameObject.transform
                    .DOMoveX(transform.position.x - 0.1f, 5f)
                    .SetEase(Ease.Linear);

                transform
                    .DOMoveX(transform.position.x + 0.1f, 5f)
                    .SetEase(Ease.Linear);
            }
        }
    }
}