using UnityEngine;

namespace Players
{
    public class PlayerState : MonoBehaviour
    {
        public enum PlayerStates
        {
            Idle,
            Move,
            Interact,
            Dialogue,
            Battle,
            Cutscene,
        }
    }
}