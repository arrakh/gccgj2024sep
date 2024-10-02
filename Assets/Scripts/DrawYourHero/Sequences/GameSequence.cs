using System;
using Cinemachine;
using DG.Tweening;
using DrawYourHero.Player;
using DrawYourHero.UI;
using DrawYourHero.Utility;
using UnityEngine;

namespace DrawYourHero.Sequences
{
    public class GameSequence : MonoBehaviour
    {
        [SerializeField] private DrawUI drawUI;
        [SerializeField] private SubtitleUI subtitleUI;
        [SerializeField] private DrawableSpriteRenderer playerSprite;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerSwordCombat playerSword; 
        
        private void Start()
        {
            playerMovement.SetCanMove(false);
            playerSword.Enable(false);
            playerSword.EnableSpin(false);
            
            DOVirtual.Float(6f, 1f, 1.2f, SetCameraOrtho).SetEase(Ease.InCubic);
            
            subtitleUI.Display("Draw your Hero", 0.6f);
            drawUI.StartDraw(playerSprite, OnDoneDrawing);
        }

        private void OnDoneDrawing()
        {
            DOVirtual.Float(1f, 6f, 1.2f, SetCameraOrtho).SetEase(Ease.InCubic);
            playerMovement.SetCanMove(true);
            playerSword.Enable(true);
            playerSword.EnableSpin(true);
        }

        private void SetCameraOrtho(float value)
        {
            virtualCamera.m_Lens.OrthographicSize = value;
        }
    }
}