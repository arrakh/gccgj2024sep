using UnityEngine;

namespace Arr.VisualEffectsController
{
    public interface IVisualEffectsHandler
    {
        public IEffect Spawn(string key, Vector3 position);
    }
}