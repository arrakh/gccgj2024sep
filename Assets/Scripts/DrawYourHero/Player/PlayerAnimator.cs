using System;
using UnityEngine;

namespace DrawYourHero.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private Animator animator;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Update()
        {
            animator.SetBool(IsMoving, movement.IsMoving);
        }
    }
}