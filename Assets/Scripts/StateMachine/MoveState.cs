using Players;
using UnityEngine;

namespace StateMachine
{
    public class MoveState : IState
    {
        private readonly PlayerController _player;
        private readonly CharacterController _controller;
        private readonly float moveSpeed;
        public MoveState(PlayerController playerController, CharacterController controller, Vector3 move, float moveSpeed)
        {
            _player = playerController;
            _controller = controller;
            this.moveSpeed = moveSpeed;
        }
        public void Enter()
        {
            _player.SetAnimation(PlayerAnimation.PlayerAnimationStates.Move);
        }

        public void Update()
        {
            _controller.Move(_player.PlayerMove * moveSpeed  * Time.deltaTime);
        }

        public void Exit()
        {
            
        }
    }
}