using System;
using UnityEngine;

namespace DrawYourHero.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 4f;

        private bool canMove = true;
        private bool isMoving;

        public bool IsMoving => isMoving;

        public void SetCanMove(bool on) => canMove = on;
        
        private void Update()
        {
            if (!canMove) return;
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var input = new Vector3(x, y).normalized;
            
            transform.position += input * (Time.deltaTime * speed);

            isMoving = input.magnitude > 0f;
        }
    }
}