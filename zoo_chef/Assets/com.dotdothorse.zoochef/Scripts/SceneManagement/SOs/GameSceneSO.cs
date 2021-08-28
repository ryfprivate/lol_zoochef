using UnityEngine;
using UnityEngine.AddressableAssets;

namespace com.dotdothorse.zoochef
{
    public enum SceneType
    {
        PersistentManagers,
        Menu,
        Gameplay,
        Level
    }

    [CreateAssetMenu(menuName = "SceneData/GameScene")]
    public class GameSceneSO: BaseDescriptionSO
    {
        public string sceneName;
        public SceneType sceneType;
        public AssetReference sceneReference;
    }
}