using System;
using DrawYourHero.Damage;
using UnityEngine;

namespace DrawYourHero.Enemy
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] private GameObject parentObject;
        [SerializeField] private int damage;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable)) return;
            damageable.Damage(DamageSource.ENEMY, parentObject, damage);
        }
    }
}