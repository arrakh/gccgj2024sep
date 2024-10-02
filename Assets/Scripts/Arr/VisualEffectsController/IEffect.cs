using UnityEngine;

namespace Arr.VisualEffectsController
{
    public interface IEffect
    {
        public void SetPosition(Vector3 position);
        public void Follow(Transform toFollow);
        public void Unfollow();
        public T GetEffectComponent<T>() where T : IEffectComponent;
        public bool TryGetEffectComponent<T>(out T component) where T : IEffectComponent;
    }
}