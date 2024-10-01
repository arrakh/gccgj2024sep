using System;
using Brushes;
using Brushes.Implementations;
using DrawYourHero.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DrawYourHero.UI
{
    public class DrawUI : MonoBehaviour
    {
        [SerializeField] private GameObject holder;
        [SerializeField] private FlexibleColorPicker colorPicker;
        [SerializeField] private float smoothness;
        [SerializeField] private Slider brushSizeSlider;
        [SerializeField] private Button endDrawButton;
        [SerializeField] private Button clearButton;
        
        private BrushData brushData; 
        private DrawableSpriteRenderer spriteRenderer;
        
        private Vector2 lastWorldPoint;
        private bool hasLastMousePosition = false;
        private Camera camera;
        private IBrush currentBrush = new CircleBrush();
        private bool isDrawing = false;
        private Action onDoneDrawing;
        
        private void Start()
        {
            camera = Camera.main;
            clearButton.onClick.AddListener(OnClear);
            endDrawButton.onClick.AddListener(StopDraw);
        }

        private void OnClear()
        {
            if (!isDrawing) return;
            
            spriteRenderer.Clear();
        }

        public void StartDraw(DrawableSpriteRenderer drawable, Action onDone)
        {
            onDoneDrawing = onDone;
            spriteRenderer = drawable;
            spriteRenderer.Clear();
            spriteRenderer.ShowSizeReference(true);
            
            holder.SetActive(true);
            

            isDrawing = true;
        }

        public void StopDraw()
        {
            isDrawing = false;
            holder.SetActive(false);
            spriteRenderer.ShowSizeReference(false);
            onDoneDrawing?.Invoke();
        }

        [ContextMenu("Save Texture")]
        void SaveTexture()
        {
            if (!isDrawing) return;
            
            TextureSave.AsPNG(spriteRenderer.CurrentTexture);
        }

        void Update()
        {
            if (!isDrawing) return;
            
            if (!Input.GetMouseButton(0))
            {
                hasLastMousePosition = false;
                return;
            }

            brushData.color = colorPicker.color;
            brushData.size = (int) brushSizeSlider.value;
        
            Vector2 worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (!hitInfo) return;

            var targetWorldPoint = hasLastMousePosition ? lastWorldPoint : worldPoint;
            spriteRenderer.Draw(brushData, currentBrush, smoothness, targetWorldPoint, worldPoint);

            lastWorldPoint = worldPoint;
            hasLastMousePosition = true;
        }
    }
}