using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Listening to")]
        [SerializeField] private ColdStartupEventChannelSO _coldStartupChannel = default;
        [SerializeField] private LoadEventChannelSO _loadLevelChannel = default;

        [Header("Broadcasting to")]
        [SerializeField] private FadeEventChannelSO _fadeChannel = default;
        [SerializeField] private GameplayEventChannelSO _gameplayChannel = default;

        private void OnEnable()
        {
            _loadLevelChannel.OnLoadingFinished += StartLevel;

#if UNITY_EDITOR
            _coldStartupChannel.OnLoadingFinished += (GameSceneSO scene) => StartLevel();
#endif
        }

        private void OnDisable()
        {
            _loadLevelChannel.OnLoadingFinished -= StartLevel;

#if UNITY_EDITOR
            _coldStartupChannel.OnLoadingFinished -= (GameSceneSO scene) => StartLevel();
#endif
        }

        private void StartLevel()
        {
            _fadeChannel.FadeIn(1f);
            _gameplayChannel.StartLevel();
        }
    }
}