using System;
using UnityEngine;

namespace Players
{
    public class PlayerAnimation : MonoBehaviour
    {
        public enum PlayerAnimationStates
        {
            Idle,
            Move,
            Walk,
            GetHit,
            Attack,
            Skill
        }
        [SerializeField] private KnightControl knight;
        [SerializeField] private PlayerAnimationStates playerAnimationState;

        public void PlayAnimation(PlayerAnimationStates states)
        {
            if(playerAnimationState == states) return;
            playerAnimationState = states;
            switch (playerAnimationState)
            {
                case PlayerAnimationStates.Idle:
                    knight.idle();
                    break;
                case PlayerAnimationStates.Move:
                    knight.running();
                    break;
                case PlayerAnimationStates.Walk:
                    knight.walking();
                    break;
                case PlayerAnimationStates.GetHit:
                    break;
                case PlayerAnimationStates.Attack:
                    break;
                case PlayerAnimationStates.Skill:
                    break;
            }
        }
    }
}