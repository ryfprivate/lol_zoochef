using UnityEngine;
using UnityEngine.Events;

namespace com.dotdothorse.zoochef
{
    [CreateAssetMenu(menuName = "Event Channels/Dialogue")]
    public class DialogueEventChannelSO : BaseDescriptionSO
    {
        public UnityAction OnDialogueDone;
        public UnityAction OnRequestStart;
        public UnityAction OnRequestHide;
        public UnityAction OnRequestReset;
        public UnityAction OnRequestNextLine;
        public UnityAction<DialogueSequenceSO> OnRequestEnqueue;

        public void DialogueDone()
        {
            if (OnDialogueDone != null)
                OnDialogueDone.Invoke();
        }

        public void RequestStart()
        {
            if (OnRequestStart != null)
                OnRequestStart.Invoke();
        }

        public void RequestHide()
        {
            if (OnRequestHide != null)
                OnRequestHide.Invoke();
        }

        public void RequestReset()
        {
            if (OnRequestReset != null)
                OnRequestReset.Invoke();
        }

        public void RequestEnqueue(DialogueSequenceSO dialogueSequence)
        {
            if (OnRequestEnqueue != null)
                OnRequestEnqueue.Invoke(dialogueSequence);
        }
    }
}