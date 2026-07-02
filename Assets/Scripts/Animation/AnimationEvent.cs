using Managers;
using UnityEngine;

namespace Animation
{
    public class AnimationEvent : MonoBehaviour
    {
        public void TriggerEvent(string eventName)
        {
            EventManager.AnimationEvent(eventName);
        }
    }
}