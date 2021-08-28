using UnityEngine;
using UnityEngine.Events;

namespace com.dotdothorse.zoochef
{
	[CreateAssetMenu(menuName = "Event Channels/Void")]
	public class VoidEventChannelSO : BaseDescriptionSO
	{
		public UnityAction OnEventRaised;

		public void RaiseEvent()
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke();
		}
	}

}