using System;
using DrawYourHero.Damage;
using UnityEngine;

namespace DrawYourHero.Player
{
    public class SwordCollider : MonoBehaviour
    {
        public event Action<IDamageable> onHitDamageable;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            onHitDamageable?.Invoke(damageable);
        }
    }
}