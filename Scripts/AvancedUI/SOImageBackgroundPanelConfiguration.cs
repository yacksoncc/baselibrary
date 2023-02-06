﻿#pragma warning disable 0649
using UnityEngine;

namespace AvancedUI
{
    [CreateAssetMenu(fileName = "ImageBackgroundconfiguration", menuName = "Panel Interfaz/Image Background configuration", order = 1)]
    public class SOImageBackgroundPanelConfiguration : ScriptableObject
    {
        [SerializeField] private bool showBackgroundImage;

        [SerializeField] private Color colorBackgroundImage;

        [SerializeField] private bool clickOverBackgroundImageClosePanel;
        
        public bool ShowBackgroundImage => showBackgroundImage;

        public Color ColorBackgroundImage => colorBackgroundImage;

        public bool ClickOverBackgroundImageClosePanel => clickOverBackgroundImageClosePanel;
    }
}