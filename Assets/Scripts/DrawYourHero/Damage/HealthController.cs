using UnityEngine;

namespace DrawYourHero.Damage
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private DamageSource source;
        
        public void Damage(DamageSource source, GameObject sourceObject, float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}