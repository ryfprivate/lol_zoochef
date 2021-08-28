using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace com.dotdothorse.zoochef
{
    public class LessonBasicsPlayer : MonoBehaviour
    {
        [SerializeField] private List<DialogueSequenceSO> _sequencesToPlay;

        [Header("Listening and broadcasting to")]
        [SerializeField] private DialogueEventChannelSO _dialogueChannel = default;
        [Header("Listening to")]
        [SerializeField] private ColdStartupEventChannelSO _coldStartupChannel = default;
        [SerializeField] private LoadEventChannelSO _loadLevelChannel = default;
        [Header("Broacasting to")]
        [SerializeField] private GameplayEventChannelSO _gameplayChannel = default;


        public bool start = false;

        private void OnEnable()
        {
            _loadLevelChannel.OnLoadingFinished += EnqueueAllSequences;
#if UNITY_EDITOR
            _coldStartupChannel.OnLoadingFinished += (GameSceneSO scene) => EnqueueAllSequences();
#endif
        }

        private void OnDisable()
        {
            _loadLevelChannel.OnLoadingFinished -= EnqueueAllSequences;
#if UNITY_EDITOR
            _coldStartupChannel.OnLoadingFinished -= (GameSceneSO scene) => EnqueueAllSequences();
#endif
        }

        private void EnqueueAllSequences()
        {
            if (_sequencesToPlay != null)
            {
                foreach (DialogueSequenceSO sequence in _sequencesToPlay)
                {
                    _dialogueChannel.RequestEnqueue(sequence);
                }
            }
        }

        private void Start()
        {
            StartCoroutine(MainSequence());
        }

        private IEnumerator MainSequence()
        {
            while (!start)
                yield return null;

            bool next = false;
            _dialogueChannel.RequestStart();
            _dialogueChannel.OnDialogueDone += () => Utils.SetConditional(ref next);

            while (!next)
                yield return null;

            _dialogueChannel.OnDialogueDone -= () => Utils.SetConditional(ref next);
            _dialogueChannel.RequestHide();
            _dialogueChannel.RequestReset();
            _gameplayChannel.LevelFinished();
        }
    }
}