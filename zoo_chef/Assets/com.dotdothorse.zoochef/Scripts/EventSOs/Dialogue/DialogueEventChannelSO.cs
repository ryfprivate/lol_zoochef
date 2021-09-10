using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.dotdothorse.zoochef
{
    [CreateAssetMenu(menuName = "Event Channels/Dialogue")]
    public class DialogueEventChannelSO : BaseDescriptionSO
    {
        public UnityAction OnQueueReady;
        public UnityAction OnDialogueDone;
        public UnityAction OnRequestStart;
        public UnityAction OnRequestNextLine;
        public UnityAction OnRequestHide;
        public UnityAction OnRequestHideDimmed;
        public UnityAction OnRequestHideChat;
        public UnityAction OnRequestReset;
        public UnityAction<List<DialogueSequenceSO>, bool> OnRequestFillUp;

        public void QueueReady()
        {
            if (OnQueueReady != null)
                OnQueueReady.Invoke();
        }

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

        public void RequestNextLine()
        {
            if (OnRequestNextLine != null)
            {
                OnRequestNextLine.Invoke();
            }
        }

        public void RequestHide()
        {
            if (OnRequestHide != null)
                OnRequestHide.Invoke();
        }

        public void RequestHideDimmed()
        {
            if (OnRequestHideDimmed != null)
                OnRequestHideDimmed.Invoke();
        }

        public void RequestHideChat()
        {
            if (OnRequestHideChat != null)
                OnRequestHideChat.Invoke();
        }

        public void RequestReset()
        {
            if (OnRequestReset != null)
                OnRequestReset.Invoke();
        }

        public void RequestFillUp(List<DialogueSequenceSO> sequences, bool atBottom = true)
        {
            if (OnRequestFillUp != null)
                OnRequestFillUp.Invoke(sequences, atBottom);
        }
    }
}