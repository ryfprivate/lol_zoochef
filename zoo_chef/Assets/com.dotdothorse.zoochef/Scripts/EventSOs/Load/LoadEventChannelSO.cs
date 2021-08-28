using UnityEngine;
using UnityEngine.Events;

namespace com.dotdothorse.zoochef
{
	[CreateAssetMenu(menuName = "Event Channels/Load")]
	public class LoadEventChannelSO : BaseDescriptionSO
	{
		public UnityAction<GameSceneSO> OnLoadingRequested;
		public UnityAction OnLoadingFinished;

		public void Request(GameSceneSO scene)
		{
			if (OnLoadingRequested != null)
			{
				OnLoadingRequested.Invoke(scene);
			}
			else
			{
				Debug.LogWarning("No one to load scene: " + scene.sceneName);
			}
		}

		public void Finish()
        {
			if (OnLoadingFinished != null)
            {
				OnLoadingFinished.Invoke();
            }
        }
	}
}