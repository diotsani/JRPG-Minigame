using Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine
{
    public class InputState : IState
    {
        private readonly PlayerController _player;
        public InputState(PlayerController playerController)
        {
            _player = playerController;
        }

        private Vector3 Move { get; set; }
        
        public void Enter()
        {
            _player.SetAnimation(PlayerAnimation.PlayerAnimationStates.Idle);
        }

        public void Update()
        {
            if (Keyboard.current != null)
            {
                if (Keyboard.current.spaceKey.isPressed)
                {
                    _player.Interact();
                    _player.Collect();
                }
                var x = 0f;
                var y = 0f;
                if (Keyboard.current.wKey.isPressed)
                {
                    y = 1f;
                }

                if (Keyboard.current.aKey.isPressed)
                {
                    x = -1f;
                }

                if (Keyboard.current.sKey.isPressed)
                {
                    y = -1f;
                }

                if (Keyboard.current.dKey.isPressed)
                {
                    x = 1f;
                }
                
                Move = new Vector3(x, 0f, y);
            }
            Move = Move.normalized;
            _player.Move(Move);
        }

        public void Exit()
        {
            Move = Vector3.zero;
            _player.Move(Move);
        }
    }
}