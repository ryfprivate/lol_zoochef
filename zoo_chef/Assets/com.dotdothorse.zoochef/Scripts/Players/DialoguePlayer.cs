using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    public class DialoguePlayer : MonoBehaviour
    {
        [SerializeField] private UIDialogue _uiDialogue;

        [Header("Listening and broadcasting to")]
        [SerializeField] private DialogueEventChannelSO _dialogueChannel = default;

        private Queue<DialogueSequenceSO> dialogueSequenceQueue;
        private Queue<string> dialogueQueue;

        private void Awake()
        {
            dialogueSequenceQueue = new Queue<DialogueSequenceSO>();
            HideAll();
        }

        private void OnEnable()
        {
            _dialogueChannel.OnRequestStart += StartDialogue;
            _dialogueChannel.OnRequestNextLine += PlayNextLine;
            _dialogueChannel.OnRequestFillUp += EnqueueSequences;
            _dialogueChannel.OnRequestHide += HideAll;
            _dialogueChannel.OnRequestHideDimmed += HideDimmed;
            _dialogueChannel.OnRequestHideChat += HideChat;
            _dialogueChannel.OnRequestReset += ResetSequences;
        }

        private void OnDisable()
        {
            _dialogueChannel.OnRequestStart -= StartDialogue;
            _dialogueChannel.OnRequestNextLine -= PlayNextLine;
            _dialogueChannel.OnRequestFillUp -= EnqueueSequences;
            _dialogueChannel.OnRequestHide -= HideAll;
            _dialogueChannel.OnRequestHideDimmed -= HideDimmed;
            _dialogueChannel.OnRequestHideChat -= HideChat;
            _dialogueChannel.OnRequestReset -= ResetSequences;
        }

        private void EnqueueSequences(List<DialogueSequenceSO> sequences)
        {
            foreach (DialogueSequenceSO sequence in sequences)
            {
                dialogueSequenceQueue.Enqueue(sequence);
            }
            _dialogueChannel.QueueReady();
        }

        private void StartDialogue()
        {
            if (dialogueSequenceQueue.Count == 0)
            {
                Debug.Log("Dialogue Player: No more sequences to play");
                return;
            }

            DialogueSequenceSO nextSequence = dialogueSequenceQueue.Dequeue();
            dialogueQueue = new Queue<string>();
            foreach (string sentence in nextSequence.sentences)
            {
                dialogueQueue.Enqueue(sentence);
            }

            _uiDialogue.RevealEntire();

            PlayNextLine();
        }

        private void PlayNextLine()
        {
            if (dialogueQueue.Count == 0)
            {
                Debug.Log("Dialogue Player: Dialogue Finished");
                _dialogueChannel.DialogueDone();
            }
            else
            {
                string nextSentence = dialogueQueue.Dequeue();
                _uiDialogue.SetText(nextSentence);
            }
        }

        private void HideDimmed()
        {
            _uiDialogue.HideDimmed();
        }

        private void HideChat()
        {
            _uiDialogue.HideChat();
        }

        private void HideAll()
        {
            _uiDialogue.HideEntire();
        }

        private void ResetSequences()
        {
            dialogueSequenceQueue = new Queue<DialogueSequenceSO>();
        }
    }
}