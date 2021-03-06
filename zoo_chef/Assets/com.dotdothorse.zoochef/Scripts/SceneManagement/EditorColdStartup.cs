using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace com.dotdothorse.zoochef
{
    public class EditorColdStartup : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private GameSceneSO _thisScene = default;
        [SerializeField] private GameSceneSO _managersScene = default;

        [Header("Broadcasting to")]
        [SerializeField] private AssetReference _coldStartupChannel = default;

        private bool isColdStart = false;
        private void Awake()
        {
            if (!SceneManager.GetSceneByName(_managersScene.sceneReference.editorAsset.name).isLoaded)
            {
                isColdStart = true;
            }
        }

        private void Start()
        {
            if (isColdStart)
            {
                _managersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
            }
        }

        private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
        {
            _coldStartupChannel.LoadAssetAsync<ColdStartupEventChannelSO>().Completed += OnChannelLoaded;
        }

        private void OnChannelLoaded(AsyncOperationHandle<ColdStartupEventChannelSO> obj)
        {
            if (_thisScene != null)
            {
                obj.Result.Request(_thisScene);
            }
        }
#endif
    }
}