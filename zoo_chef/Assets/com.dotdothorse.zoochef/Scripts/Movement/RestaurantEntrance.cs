using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace com.dotdothorse.zoochef
{
    public class RestaurantEntrance : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private List<Transform> _backgrounds;

        public bool on = false;

        private void Move()
        {
            _cam.gameObject.transform
                .DOMoveX(transform.position.x - 0.3f, 4f)
                .SetEase(Ease.Linear);

            float moveAmount = 0.8f;

            foreach (Transform bg in _backgrounds)
            {
                Debug.Log("move " + moveAmount);
                bg
                    .DOMoveX(bg.position.x + moveAmount, 4f)
                    .SetEase(Ease.Linear);
                moveAmount -= 0.2f;
            }
            on = false;
        }

        private void Update()
        {
            if (on)
            {
                Move();
            }
        }
    }
}