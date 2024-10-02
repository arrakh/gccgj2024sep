using System;
using DrawYourHero.Damage;
using UnityEngine;

namespace DrawYourHero.Player
{
    public class PlayerSwordCombat : MonoBehaviour
    {
        [SerializeField] private GameObject sword;
        [SerializeField] private float spinSpeed = 360f;
        [SerializeField] private SwordCollider swordCollider;
        [SerializeField] private int baseDamage = 1;
        [SerializeField] private float boostCooldown = 3f;
        [SerializeField] private float boostDuration = 0.6f;
        [SerializeField] private float boostExtraSpeed = 720f;
        [SerializeField] private AnimationCurve boostDecayCurve;

        private bool shouldSpin;

        private float currentBoostCooldown;
        private float currentBoostDuration;
        private void Awake()
        {
            swordCollider.onHitDamageable += OnHit;
        }

        private void OnHit(IDamageable damageable)
        {
            damageable.Damage(DamageSource.PLAYER, gameObject, baseDamage);
        }

        private void Update()
        {
            SpinToWin();
            BoostUpdate();
            BoostDurationUpdate();
        }

        private void BoostDurationUpdate()
        {
            currentBoostDuration -= Time.deltaTime;
        }

        private void BoostUpdate()
        {
            currentBoostCooldown -= Time.deltaTime;
            if (currentBoostCooldown > 0f) return;

            if (Input.GetKeyDown(KeyCode.Space)) Boost();
        }

        private void Boost()
        {
            currentBoostDuration = boostDuration;
            currentBoostCooldown = boostCooldown;
        }

        public void Enable(bool on) => sword.SetActive(on);

        public void EnableSpin(bool on) => shouldSpin = on;

        private void SpinToWin()
        {
            if (!shouldSpin) return;

            var boostAlpha = 1f - Mathf.Clamp01(currentBoostDuration / boostDuration);
            var extraSpeed = boostDecayCurve.Evaluate(boostAlpha) * boostExtraSpeed;
            var speed = (spinSpeed + extraSpeed) * Time.deltaTime;
            sword.transform.Rotate(0f, 0f, speed);
        }

        /*private void LookToMouse()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z;
            var worldPoint = camera.ScreenToWorldPoint(mousePosition);
            var pos = transform.position;
            lastAngle = Mathf.Atan2(pos.y - worldPoint.y, pos.x - worldPoint.x) * Mathf.Rad2Deg;
            lastAngle += 90f;
            aimPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, lastAngle));
        }*/
    }
}