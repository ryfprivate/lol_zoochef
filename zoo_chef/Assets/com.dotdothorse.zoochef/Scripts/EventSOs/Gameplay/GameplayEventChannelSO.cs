using UnityEngine;
using UnityEngine.Events;

namespace com.dotdothorse.zoochef
{
    [CreateAssetMenu(menuName = "Event Channels/Gameplay")]
    public class GameplayEventChannelSO : BaseDescriptionSO
    {
        public UnityAction OnLevelFinished;

        public void LevelFinished()
        {
            if (OnLevelFinished != null)
                OnLevelFinished.Invoke();
        }
    }
}