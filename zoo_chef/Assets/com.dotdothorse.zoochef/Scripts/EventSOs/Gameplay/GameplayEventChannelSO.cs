using UnityEngine;
using UnityEngine.Events;

namespace com.dotdothorse.zoochef
{
    [CreateAssetMenu(menuName = "Event Channels/Gameplay")]
    public class GameplayEventChannelSO : BaseDescriptionSO
    {
        public UnityAction OnLevelStart;
        public UnityAction OnChallengeCompleted;
        public UnityAction OnLevelFinished;

        public void StartLevel()
        {
            if (OnLevelStart != null)
                OnLevelStart.Invoke();
        }

        public void ChallengeCompleted()
        {
            if (OnChallengeCompleted != null)
                OnChallengeCompleted.Invoke();
        }

        public void LevelFinished()
        {
            if (OnLevelFinished != null)
                OnLevelFinished.Invoke();
        }
    }
}