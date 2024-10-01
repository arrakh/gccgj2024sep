using System;
using UnityEngine;

namespace DrawYourHero.Player
{
    public class PlayerSwordCombat : MonoBehaviour
    {
        [SerializeField] private GameObject sword;
        [SerializeField] private Transform aimPivot;
        [SerializeField] private ParticleSystem swingParticle;
        [SerializeField] private Animator swordAnimator;
        [SerializeField] private float cooldown;

        private float currentCooldown;

        private Camera camera;

        private void Awake()
        {
            camera = Camera.main;
            currentCooldown = cooldown;
        }

        private void Update()
        {
            LookToMouse();
            if (IsCooldownUpdate()) return;
        }

        private bool IsCooldownUpdate()
        {
            currentCooldown -= Time.deltaTime;
            return currentCooldown > 0f;
        }

        private void LookToMouse()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z;
            var worldPoint = camera.ScreenToWorldPoint(mousePosition);
            var pos = transform.position;
            var angle = Mathf.Atan2(pos.y - worldPoint.y, pos.x - worldPoint.x) * Mathf.Rad2Deg;
            aimPivot.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
        }
    }
}