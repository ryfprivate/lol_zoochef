using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
 
namespace com.dotdothorse.zoochef
{
    public class SpawnPrefab : MonoBehaviour
    {
        [SerializeField] private AssetReference prefabReference;

        public GameObject instance;

        private void OnEnable()
        {
            var asyncOperationHandle = prefabReference.InstantiateAsync();
            asyncOperationHandle.Completed += handle => instance = handle.Result;
        }

        private void OnDisable()
        {
            Addressables.ReleaseInstance(instance);
        }
    }
}