using System;
using Cysharp.Threading.Tasks;
using Managers;
using Players;
using Save;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Cutscene
{
    public class CutsceneManager : MonoBehaviour
    {
        public static CutsceneManager Instance;
        [Header("Data")]
        [SerializeField] private CutsceneData introData;
        [Header("Manager")]
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private DialogueManager dialogueManager;
        [Header("Move")]
        [SerializeField] private MoveCommandData[] moveCommandsData;
        [SerializeField] private PlayerFollower follower1;
        [Header("Event")]
        [SerializeField] private EventCommandData[] eventCommandsData;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (DataManager.Instance.CheckCutscene(introData.cutsceneId))
            {
                PlayerController.Instance.ChangeInputState();
            }
            else
            {
                _ = Play(introData);
            }
        }

        public async UniTask Play(CutsceneData data)
        {
            PlayerController.Instance.ChangeCutsceneState();
            
            CutsceneContext context = new CutsceneContext
            {
                Cutscene = this,
                Camera = cameraManager,
                Dialogue = dialogueManager,
                Data = DataManager.Instance
            };
            foreach (var cmd in data.commands)
            {
                await cmd.Execute(context);
            }
            
            DataManager.Instance.AddCutscene(data.cutsceneId);
            SaveSystem.Save();
            PlayerController.Instance.ChangeInputState();
        }

        public async UniTask PlayMove(MoveCommandActor actor, MoveCommandTarget target)
        {
            var cmd = moveCommandsData[(int)target];
            switch (actor)
            {
                case MoveCommandActor.Player:
                    await PlayerController.Instance.MoveTo(cmd.targetTransform.position);
                    break;
                case MoveCommandActor.Follower1:
                    await follower1.MoveTo(cmd.targetTransform.position);
                    break;
                case MoveCommandActor.Follower2:
                    break;
                default:
                    await PlayerController.Instance.MoveTo(cmd.targetTransform.position);
                    break;
            }
        }
        
        public async UniTask PlayEvent(EventCommandHandler handler)
        {
            EventCommandData cmd = eventCommandsData[(int)handler];
            cmd.completed = false;
            cmd.eventHandler?.Invoke();
            await UniTask.WaitUntil(() => cmd.completed);
        }
        
        public void EventComplete(EventCommandHandler handler)
        {
            eventCommandsData[(int)handler].completed = true;
        }
    }

    public enum MoveCommandActor
    {
        Player,
        Follower1,
        Follower2,
    }
    
    public enum MoveCommandTarget
    {
        Intro0 = 0,
        Intro1 = 1,
    }

    public enum EventCommandHandler
    {
        Intro0 = 0,
    }

    public enum WaitCommandType
    {
        Time,
        Bool
    }
    
    public enum RewardCommandType
    {
        Consumable,
        Equipment,
        Miscellaneous,
        Quest,
    }
    
    public enum CameraCommandTarget
    {
        PlayerVCam = 0,
        GroupVCam = 1
    }
    
    [Serializable]
    public class MoveCommandData
    {
        public Transform targetTransform;
    }
    [Serializable]
    public class EventCommandData
    {
        public UnityEvent eventHandler;
        public bool completed;
    }
}