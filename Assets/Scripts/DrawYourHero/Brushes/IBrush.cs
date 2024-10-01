using UnityEngine;

namespace Brushes
{
    public interface IBrush
    {
        void Draw(Color[] pixels, BrushData brush, Vector2Int brushPos, int width, int height);
    }
}