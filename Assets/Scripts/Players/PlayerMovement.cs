using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Players
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float rotation = 10f;
        private CharacterController _controller;
        private PlayerInput _input;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            Vector3 move = _input.Move;
            
            _controller.Move(move * speed  * Time.deltaTime);

            //Rotate(move);
        }

        private void Rotate(Vector3 move)
        {
            if(move.sqrMagnitude < 0.01f)return;
            
            Quaternion rot = Quaternion.LookRotation(move);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotation * Time.deltaTime);
        }
    }
}