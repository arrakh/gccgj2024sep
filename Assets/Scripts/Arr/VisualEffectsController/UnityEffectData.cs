using System;
using UnityEngine;

namespace Arr.VisualEffectsController
{
    [Serializable]
    public struct UnityEffectData
    {
        public GameObject prefab;
        public int initialSize;
        public int maxSize;
        public float timeoutDuration;
    }
}