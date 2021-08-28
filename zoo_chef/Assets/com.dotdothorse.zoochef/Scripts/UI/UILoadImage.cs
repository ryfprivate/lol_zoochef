using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

namespace com.dotdothorse.zoochef
{
    public class UILoadImage : MonoBehaviour
    {
        [SerializeField] private AssetReference _spriteReference;
        [SerializeField] private Image _image;

        private AsyncOperationHandle<Sprite> _currentSpriteHandle;

        private void OnEnable()
        {
            _currentSpriteHandle = _spriteReference.LoadAssetAsync<Sprite>();
            _currentSpriteHandle.Completed += SetImage;
        }

        private void OnDisable()
        {
            if (_currentSpriteHandle.IsValid())
            {
                Addressables.Release(_currentSpriteHandle);
            }
        }

        void SetImage(AsyncOperationHandle<Sprite> obj)
        {
            _image.sprite = obj.Result;
        }
    }
}