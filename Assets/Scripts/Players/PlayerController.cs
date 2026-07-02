using Cysharp.Threading.Tasks;
using Game.Collectible;
using Interfaces;
using Managers;
using StateMachine;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        public StateMachine.StateMachine StateMachine { get; private set; }
        public InputState InputState { get; private set; }
        public MoveState MoveState { get; private set; }
        public CutsceneState CutsceneState { get; private set; }
        public InteractState InteractState { get; private set; }
        
        private IInteractable _currentInteractable;
        private ICollectible _currentCollectible;
    
        [SerializeField] private CharacterController _controller;
        private PlayerAnimation _playerAnimation;
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotation = 10f;
        public float MoveSpeed => moveSpeed;
        public float Rotation => rotation;
        private Vector3 _playerMove = Vector3.zero;
        public Vector3 PlayerMove => _playerMove;

        private void Awake()
        {
            Instance = this;
            //_controller = GetComponent<CharacterController>();
            _playerAnimation = GetComponent<PlayerAnimation>();
        
            StateMachine = new StateMachine.StateMachine();
        
            InputState = new InputState(this);
            //MoveState = new MoveState(this, _controller, _playerMove, moveSpeed);
            InteractState = new InteractState(this);
        }
        
        private void OnEnable()
        {
            EventManager.OnStopDialogueNpc += StopDialogueNpc;
        }

        private void OnDisable()
        {
            EventManager.OnStopDialogueNpc -= StopDialogueNpc;
        }

        private void Start()
        {
            //ChangeInputState();
        }

        private void Update()
        {
            StateMachine.Update();
        }

        public void ChangeCutsceneState()
        {
            Debug.Log("Player change state cutscene");
            StateMachine.ChangeState(CutsceneState);
        }
        
        public void ChangeInputState()
        {
            Debug.Log("Player change state input");
            StateMachine.ChangeState(InputState);
        }

        public void SetAnimation(PlayerAnimation.PlayerAnimationStates state)
        {
            _playerAnimation.PlayAnimation(state);
        }
    
        public void Move(Vector3 move)
        {
            var moveState = move != Vector3.zero ? PlayerAnimation.PlayerAnimationStates.Move : PlayerAnimation.PlayerAnimationStates.Idle;
            SetAnimation(moveState);
            _playerMove = move;
            _controller.Move(_playerMove * moveSpeed  * Time.deltaTime);
            Rotate();
        }

        public void ControllerMove(Vector3 move, Vector3 playerMove, Quaternion look)
        {
            _playerMove = playerMove;
            _look = look;
            _controller.transform.position = move;
            //_controller.Move(move);
            Rotate();
        }

        public async UniTask MoveTo(Vector3 to)
        {
            SetAnimation(PlayerAnimation.PlayerAnimationStates.Walk);
            while (Vector3.Distance(_controller.transform.position, to) > 0.01f)
            {
                Vector3 dir = (to - _controller.transform.position).normalized;
                _controller.Move(dir * (moveSpeed / 2) * Time.deltaTime);
                await UniTask.Yield();
            }
            SetAnimation(PlayerAnimation.PlayerAnimationStates.Idle);
        }

        private Quaternion _look = Quaternion.Euler(0,0,0);
        public Quaternion  Look => _look;
        private void Rotate()
        {
            _look = _playerMove.x switch
            {
                1 => Quaternion.Euler(0, 0, 0),
                -1 => Quaternion.Euler(0, 180, 0),
                _ => _look
            };
            transform.rotation = Quaternion.Slerp(transform.rotation, _look, rotation * Time.deltaTime);
        }

        public void Interact()
        {
            if(_currentInteractable == null)return;
            StateMachine.ChangeState(InteractState);
            _currentInteractable.Interact();
        }

        public void Collect()
        {
            if(_currentCollectible == null)return;
            _currentCollectible.Collected();
            _currentCollectible = null;
        }
        
        private void StopDialogueNpc()
        {
            if(_currentInteractable == null)return;
            ChangeInputState();
            _currentInteractable = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log($"Player entered trigger with {interactable}");
                _currentInteractable = interactable;
            }
            
            if (other.TryGetComponent(out ICollectible collectible))
            {
                Debug.Log($"Player entered trigger with {collectible}");
                _currentCollectible = collectible;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log($"Player exited trigger with {interactable}");
                ChangeInputState();
                _currentInteractable = null;
            }

            if (other.TryGetComponent(out ICollectible collectible))
            {
                Debug.Log($"Player exited trigger with {collectible}");
                _currentCollectible = null;
            }
        }
    }
}
