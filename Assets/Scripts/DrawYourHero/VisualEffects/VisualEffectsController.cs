using System;
using System.Collections.Generic;
using Arr.VisualEffectsController;
using UnityEngine;

namespace DrawYourHero.VisualEffects
{
    [Serializable]
    public struct EffectPair
    {
        public string id;
        public UnityEffectData data;
    }
    
    public class VisualEffectsController : MonoBehaviour
    {
        public static VisualEffectsController Instance;

        [SerializeField] private EffectPair[] effects;

        private Dictionary<string, UnityEffectPool> pools = new();
        
        private void Awake()
        {
            foreach (var pair in effects)
            {
                var pool = new UnityEffectPool(pair.data);
                pools[pair.id] = pool;
            }
            
            Instance = this;
        }

        public UnityEffect Spawn(string key, Vector3 worldPos)
        {
            if (!pools.TryGetValue(key, out var pool))
                throw new Exception($"Trying to spawn Effect {key} but it does not exist!");
            
            var effect = pool.Get();
            effect.transform.position = worldPos;

            return effect;
        }
    }
}