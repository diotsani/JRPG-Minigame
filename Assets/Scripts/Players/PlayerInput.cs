using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 Move { get; private set; }

        private void Update()
        {
            if (Keyboard.current != null)
            {
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
        }
    }
}
