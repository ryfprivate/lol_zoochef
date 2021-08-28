using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    [CreateAssetMenu(menuName = "SceneData/LevelScene")]
    public class LevelSceneSO : GameSceneSO
    {
        public LevelSceneSO _nextLevel;
    }
}