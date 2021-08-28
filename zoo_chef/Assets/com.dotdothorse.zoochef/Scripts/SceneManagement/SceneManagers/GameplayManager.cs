using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.dotdothorse.zoochef
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("Main menu scene")]
        [SerializeField] GameSceneSO _menuScene;
        [Header("Level scenes")]
        [SerializeField] LevelSceneSO _firstLevel;

        [Header("Listening to")]
        [SerializeField] private ColdStartupEventChannelSO _coldStartupChannel = default;
        [SerializeField] private LoadEventChannelSO _loadGameplayChannel = default;
        [SerializeField] private GameplayEventChannelSO _gameplayChannel = default;

        [Header("Broadcasting to")]
        [SerializeField] private LoadEventChannelSO _loadMenuChannel = default;
        [SerializeField] private LoadEventChannelSO _loadLevelChannel = default;
        [SerializeField] private FadeEventChannelSO _fadeChannel = default;

        [Header("Read-only")]
        public LevelSceneSO currentLevel;

        private void OnEnable()
        {
            _loadGameplayChannel.OnLoadingFinished += LoadFirstLevel;
            _gameplayChannel.OnLevelFinished += LoadNextLevel;

#if UNITY_EDITOR
            _coldStartupChannel.OnLoadingFinished += SetCurrentLevel;
#endif
        }

        private void OnDisable()
        {
            _loadGameplayChannel.OnLoadingFinished -= LoadFirstLevel;
            _gameplayChannel.OnLevelFinished -= LoadNextLevel;

#if UNITY_EDITOR
            _coldStartupChannel.OnLoadingFinished -= SetCurrentLevel;
#endif
        }

        private void SetCurrentLevel(GameSceneSO scene)
        {
            if (scene.sceneType == SceneType.Level)
            {
                currentLevel = (LevelSceneSO)scene;
            }
        }

        private void LoadFirstLevel()
        {
            _loadLevelChannel.Request(_firstLevel);
            currentLevel = _firstLevel;
        }

        private void LoadNextLevel()
        {
            StartCoroutine(C_LoadNextLeveL());
        }

        private IEnumerator C_LoadNextLeveL()
        {
            float fadeOutDuration = 1f;
            _fadeChannel.FadeOut(fadeOutDuration);
            yield return new WaitForSeconds(fadeOutDuration);

            UnloadLevel(currentLevel);

            if (currentLevel._nextLevel == null)
            {
                _loadMenuChannel.Request(_menuScene);
                yield return null;
            }

            LevelSceneSO nextLevel = currentLevel._nextLevel;
            _loadLevelChannel.Request(nextLevel);
            currentLevel = nextLevel;
        }

        private void UnloadLevel(LevelSceneSO level)
        {
            if (level != null)
            {
                if (level.sceneReference.OperationHandle.IsValid())
                {
                    //Unload the scene through its AssetReference, i.e. through the Addressable system
                    level.sceneReference.UnLoadScene();
                }
#if UNITY_EDITOR
                else
                {
                    //Only used when, after a "cold start", the player moves to a new scene
                    //Since the AsyncOperationHandle has not been used (the scene was already open in the editor),
                    //the scene needs to be unloaded using regular SceneManager instead of as an Addressable
                    SceneManager.UnloadSceneAsync(level.sceneReference.editorAsset.name);
                }
#endif
            }
        }
    }
}