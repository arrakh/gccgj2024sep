using UnityEngine;

namespace Brushes.Implementations
{
    public class CircleBrush : IBrush
    {
        public void Draw(Color[] pixels, BrushData brush, Vector2Int brushPos, int width, int height)
        {
            for (int x = -brush.size; x <= brush.size; x++)
            {
                for (int y = -brush.size; y <= brush.size; y++)
                {
                    // Calculate the exact distance from the center of the circle
                    float distance = Mathf.Sqrt(x * x + y * y);

                    // Check if the pixel is near the edge of the circle
                    if (!(distance <= brush.size + 1)) continue; // Allow a small buffer for anti-aliasing
                
                    int pixelX = brushPos.x + x;
                    int pixelY = brushPos.y + y;

                    // Ensure the pixel is within the texture bounds
                    if (pixelX < 0 || pixelX >= width || pixelY < 0 || pixelY >= height) continue;
                
                    // Calculate how close this pixel is to the boundary
                    float alpha = 1 - Mathf.Clamp01(distance / brush.size);

                    // Blend the current color with the background color, adjusting the alpha for anti-aliasing
                    Color existingColor = pixels[pixelX + pixelY * width];
                    Color blendedColor = Color.Lerp(existingColor, brush.color, Mathf.Pow(alpha, brush.falloff));

                    // Set the pixel color, blending with the existing color
                    pixels[pixelX + pixelY * width] = blendedColor;
                }
            }
        }
    }
}