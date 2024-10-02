using System;
using System.Collections;
using System.Collections.Generic;
using Brushes;
using UnityEngine;

public class DrawableSpriteRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D collider2D;
    [SerializeField] private GameObject sizeReference;
    [SerializeField] private int pixelsPerUnit;

    private readonly Vector2 pivot = new (0.5f, 0.5f);

    private Vector2Int size;
    private int arraySize;

    private Color[] currentPixels;

    public Texture2D CurrentTexture => spriteRenderer.sprite.texture;
    public Color[] CurrentPixels => currentPixels;

    public void ShowSizeReference(bool show)
    {
        sizeReference.SetActive(show);
    }

    private void Awake()
    {
        var scale = transform.localScale;
        size.x = Mathf.RoundToInt(pixelsPerUnit * scale.x);
        size.y = Mathf.RoundToInt(pixelsPerUnit * scale.y);
        arraySize = size.x * size.y;
    }

    public void Clear()
    {
        var colors = new Color[arraySize];
        for (int i = 0; i < arraySize; i++)
            colors[i] = Color.clear;

        currentPixels = colors;
        
        ApplyPixels(colors);
    }

    public void Draw(BrushData brushData, IBrush brush, float smoothness, Vector2 startPos, Vector2 endPos)
    {
        var oldSprite = spriteRenderer.sprite;
        var oldTexture = oldSprite.texture;

        var newTexture2D = DrawInternal(brushData, brush, smoothness, oldTexture,  startPos, endPos);

        spriteRenderer.sprite = Sprite.Create(newTexture2D, oldSprite.rect, pivot, pixelsPerUnit);

        Destroy(oldSprite);
        Destroy(oldTexture);
    }
    
    private Texture2D DrawInternal(BrushData data, IBrush brush, float smoothness, Texture2D copiedTexture2D, Vector2 startPos, Vector2 endPos)
    {
        //STRUCTURE THIS BETTER TO ACCOMODATE SHAPE BRUSHES
        Texture2D texture = GetNewTexture();

        Color[] originalPixels = copiedTexture2D.GetPixels();
        Color[] newPixels = new Color[originalPixels.Length];
        originalPixels.CopyTo(newPixels, 0);

        // Interpolate between the start and end points, ensuring smooth drawing
        float distance = Vector2.Distance(startPos, endPos);
        int steps = Mathf.Clamp(Mathf.CeilToInt(distance * smoothness), 1, Int32.MaxValue);
        var bounds = collider2D.bounds;

        for (int i = 0; i <= steps; i++)
        {
            float t = (float)i / steps;
            Vector2 interpolatedPoint = Vector2.Lerp(startPos, endPos, t);

            // Convert the interpolated point to pixel coordinates
            int pixelX = (int)((interpolatedPoint.x - bounds.min.x) * (copiedTexture2D.width / bounds.size.x));
            int pixelY = (int)((interpolatedPoint.y - bounds.min.y) * (copiedTexture2D.height / bounds.size.y));

            var pos = new Vector2Int(pixelX, pixelY);
            
            brush.Draw(newPixels, data, pos, texture.width, texture.height);
        }

        currentPixels = newPixels;

        texture.SetPixels(newPixels);
        texture.Apply();
        
        return texture;
    }

    public void ApplyPixels(Color[] pixels)
    {
        if (spriteRenderer.sprite != null && spriteRenderer.sprite.texture != null)
        {
            Destroy(spriteRenderer.sprite.texture);
            spriteRenderer.sprite = null;
        }

        var tex = GetNewTexture();

        tex.SetPixels(pixels);
        tex.Apply();
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), pivot, pixelsPerUnit);
        currentPixels = pixels;
    }

    private Texture2D GetNewTexture()
    {
        var tex = new Texture2D(size.x, size.y);
        
        tex.filterMode = FilterMode.Bilinear;
        tex.wrapMode = TextureWrapMode.Clamp;

        return tex;
    }

}
