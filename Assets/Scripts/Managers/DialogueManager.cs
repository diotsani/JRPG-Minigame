using System;
using Cysharp.Threading.Tasks;
using Fungus;
using UnityEngine;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Flowchart flowchart;
        [SerializeField] private Flowchart npcFlowchart;

        private void OnEnable()
        {
            EventManager.OnPlayDialogueNpc += PlayDialogueNpc;
        }

        private void OnDisable()
        {
            EventManager.OnPlayDialogueNpc -= PlayDialogueNpc;
        }

        public async UniTask Play(string block)
        {
            flowchart.ExecuteBlock(block);
            await UniTask.WaitUntil(() => !flowchart.HasExecutingBlocks());
        }

        private void PlayDialogueNpc(string block)
        {
            _ = PlayDialogueNpcAsync(block);
        }

        private async UniTask PlayDialogueNpcAsync(string block)
        {
            npcFlowchart.ExecuteBlock(block);
            await UniTask.WaitUntil(() => !npcFlowchart.HasExecutingBlocks());
            EventManager.StopDialogueNpc();
        }
    }
}