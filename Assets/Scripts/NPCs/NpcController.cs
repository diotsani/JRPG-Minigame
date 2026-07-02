using System;
using Interfaces;
using Managers;
using UnityEngine;

namespace NPCs
{
    public class NpcController : MonoBehaviour, IMoveable, IInteractable
    {
        public enum NpcStates
        {
            Idle,
            Move,
            Interact,
            Dialogue,
            Battle,
            Cutscene,
        }
        
        [SerializeField] private string npcName;
        [SerializeField] private string npcBlock;
        [SerializeField] private NpcStates npcState = NpcStates.Idle;
        private CharacterController _controller;
        private NpcAnimation _npcAnimation;

        private void OnEnable()
        {
            EventManager.OnStopDialogueNpc += StopDialogueNpc;
        }

        private void OnDisable()
        {
            EventManager.OnStopDialogueNpc -= StopDialogueNpc;
        }

        public void Move(Vector3 direction)
        {
            npcState = NpcStates.Move;
        }
        
        public void Interact()
        {
            npcState = NpcStates.Dialogue;
            EventManager.PlayDialogueNpc(npcBlock);
        }

        private void FacePlayer()
        {
            
        }

        private void StopDialogueNpc()
        {
            if (npcState == NpcStates.Dialogue)
            {
                npcState = NpcStates.Idle;
            }
        }
    }
}
