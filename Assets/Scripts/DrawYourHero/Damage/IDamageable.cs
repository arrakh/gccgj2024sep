using UnityEngine;

namespace DrawYourHero.Damage
{
    public interface IDamageable
    {
        public void Damage(DamageSource source, GameObject sourceObject, float damage);
    }
}