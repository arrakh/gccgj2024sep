using System;
using UnityEngine;

namespace Brushes
{
    public struct BrushData
    {
        public Color color; //SHOULD BE READONLY
        public int size;
        public readonly int falloff;
    }
}