using System;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        public static event Action OnOpenInventory;
        public static void OpenInventory() => OnOpenInventory?.Invoke();
        
        public static event Action OnCloseInventory;
        public static void CloseInventory() => OnCloseInventory?.Invoke();
        
        public static event Action OnOpenEquipment;
        public static void OpenEquipment() => OnOpenEquipment?.Invoke();
        
        public static event Action OnCloseEquipment;
        public static void CloseEquipment() => OnCloseEquipment?.Invoke();
        
        public static event Action<string> OnPlayDialogueNpc;
        public static void PlayDialogueNpc(string block) => OnPlayDialogueNpc?.Invoke(block);
        
        public static event Action OnStopDialogueNpc;
        public static void StopDialogueNpc() => OnStopDialogueNpc?.Invoke();
        
        public static event Action<string> OnAnimationEvent;
        public static void AnimationEvent(string eventName) => OnAnimationEvent?.Invoke(eventName);
    }
}