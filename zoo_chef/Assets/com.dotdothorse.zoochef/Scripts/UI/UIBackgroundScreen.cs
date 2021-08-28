using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private void LoadCustomerScreen()
        {
            _currentSpriteHandle = _customerScreenSpriteReference.LoadAssetAsync<Sprite>();
            _currentSpriteHandle.Completed += SetSprite;
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

        public void RevealCustomerScreen()
        {
            LoadCustomerScreen();
        }

        public void HideCustomerScreen()
        {
            float fadeDuration = 1f;
            _spriteRenderer
                .DOFade(0, fadeDuration)
                .SetEase(Ease.OutExpo);
            StartCoroutine(UnloadSprite(fadeDuration));
        }

        void SetSprite(AsyncOperationHandle<Sprite> obj)
        {
            _spriteRenderer.sprite = obj.Result;
            _spriteRenderer
                .DOFade(1, 1f)
                .SetEase(Ease.InExpo);
        }
    }
}