using System;
using DrawYourHero.VisualEffects;
using UnityEngine;

namespace DrawYourHero.Damage
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private DamageSource source;
        [SerializeField] private int health;

        public event Action onDeath;
        
        public void Damage(DamageSource dmgSource, GameObject sourceObject, int damage)
        {
            if (dmgSource.Equals(source)) return;

            health -= damage;

            if (health <= 0)
            {
                onDeath?.Invoke();
                return;
            }

            VisualEffectsController.Instance.Spawn("cartoon-hit", transform.position);
        }
    }
}