using Players;

namespace StateMachine
{
    public class InteractState : IState
    {
        private readonly PlayerController _player;
        public InteractState(PlayerController playerController)
        {
            _player = playerController;
        }
        public void Enter()
        {
            _player.SetAnimation(PlayerAnimation.PlayerAnimationStates.Idle);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}