using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;
using DG.Tweening;

namespace com.dotdothorse.zoochef
{
    public class UIBackgroundScreen : MonoBehaviour
    {
        [SerializeField] private AssetReference _customerScreenSpriteReference;
        [SerializeField] private AssetReference _victoryScreenSpriteReference;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private AsyncOperationHandle<Sprite> _currentSpriteHandle;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LoadCustomerScreen(float duration)
        {
            _currentSpriteHandle = _customerScreenSpriteReference.LoadAssetAsync<Sprite>();
            _currentSpriteHandle.Completed +=
                (AsyncOperationHandle<Sprite> obj) => {
                    _spriteRenderer.sprite = obj.Result;
                    _spriteRenderer
                        .DOFade(1, duration)
                        .SetEase(Ease.InExpo);
                };
        }

        private void LoadVictoryScreen(float duration)
        {
            _currentSpriteHandle = _victoryScreenSpriteReference.LoadAssetAsync<Sprite>();
            _currentSpriteHandle.Completed +=
                (AsyncOperationHandle<Sprite> obj) => {
                    _spriteRenderer.sprite = obj.Result;
                    _spriteRenderer
                        .DOFade(1, duration)
                        .SetEase(Ease.InExpo);
                };
        }

        private IEnumerator UnloadSprite(float wait)
        {
            yield return new WaitForSeconds(wait);
            _spriteRenderer.sprite = null;
            if (_currentSpriteHandle.IsValid())
            {
                Addressables.Release(_currentSpriteHandle);
            }
        }

        public void RevealVictoryScreen(float duration)
        {
            LoadVictoryScreen(duration);
        }

        public void RevealCustomerScreen(float duration)
        {
            LoadCustomerScreen(duration);
        }

        public void HideCustomerScreen()
        {
            float fadeDuration = 1f;
            _spriteRenderer
                .DOFade(0, fadeDuration)
                .SetEase(Ease.OutExpo);
            StartCoroutine(UnloadSprite(fadeDuration));
        }
    }
}