using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Players
{
    public class PlayerFollower : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        private PlayerController _playerController;
        private PlayerAnimation _playerAnimation;
        
        private Vector3 _playerMove = Vector3.zero;
        private Quaternion _look = Quaternion.Euler(0,0,0);

        private void Awake()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        private void Start()
        {
            _playerController = PlayerController.Instance;
        }

        public void SetAnimation(PlayerAnimation.PlayerAnimationStates state)
        {
            _playerAnimation.PlayAnimation(state);
        }
        
        public async UniTask MoveTo(Vector3 to)
        {
            SetAnimation(PlayerAnimation.PlayerAnimationStates.Walk);
            while (Vector3.Distance(_controller.transform.position, to) > 0.01f)
            {
                Vector3 dir = (to - _controller.transform.position).normalized;
                _playerMove = dir;
                _controller.Move(dir * (_playerController.MoveSpeed / 2) * Time.deltaTime);
                Rotate();
                await UniTask.Yield();
            }
            SetAnimation(PlayerAnimation.PlayerAnimationStates.Idle);
        }
        
        private void Rotate()
        {
            _look = _playerMove.x switch
            {
                1 => Quaternion.Euler(0, 0, 0),
                -1 => Quaternion.Euler(0, 180, 0),
                _ => _look
            };
            transform.rotation = Quaternion.Slerp(transform.rotation, _look, _playerController.Rotation * Time.deltaTime);
        }
    }
}