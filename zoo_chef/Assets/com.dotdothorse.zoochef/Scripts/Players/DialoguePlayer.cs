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
            HideDialogue();
        }

        private void OnEnable()
        {
            _dialogueChannel.OnRequestStart += StartDialogue;
            _dialogueChannel.OnRequestEnqueue += EnqueueDialogue;
            _dialogueChannel.OnRequestHide += HideDialogue;
            _dialogueChannel.OnRequestReset += ResetSequences;
        }

        private void OnDisable()
        {
            _dialogueChannel.OnRequestStart -= StartDialogue;
            _dialogueChannel.OnRequestEnqueue -= EnqueueDialogue;
            _dialogueChannel.OnRequestHide -= HideDialogue;
            _dialogueChannel.OnRequestReset -= ResetSequences;
        }

        private void EnqueueDialogue(DialogueSequenceSO sequence)
        {
            dialogueSequenceQueue.Enqueue(sequence);
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

            _uiDialogue.RegisterButton(() => PlayNextLine());
            _uiDialogue.SetSprite(nextSequence.sprite);
            _uiDialogue.RevealEntire();

            PlayNextLine();
        }

        private void PlayNextLine()
        {
            if (dialogueQueue.Count == 0)
            {
                Debug.Log("Dialogue Player: Dialogue Finished");
                _uiDialogue.HideDimmed();
                _dialogueChannel.DialogueDone();
            }
            else
            {
                string nextSentence = dialogueQueue.Dequeue();
                _uiDialogue.SetText(nextSentence);
            }
        }

        private void HideDialogue()
        {
            _uiDialogue.HideEntire();
        }

        private void ResetSequences()
        {
            dialogueSequenceQueue = new Queue<DialogueSequenceSO>();
        }
    }
}