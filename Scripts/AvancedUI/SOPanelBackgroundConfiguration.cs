using UnityEngine;

namespace AvancedUI
{
   [CreateAssetMenu(fileName = "SOPanelBackgroundConfiguration", menuName = "PanelUI/SOPanelBackgroundConfiguration", order = 1)]
   public class SOPanelBackgroundConfiguration : ScriptableObject
   {
      [SerializeField]
      private bool showBackgroundImage;

      [SerializeField]
      private Color colorBackgroundImage;

      [SerializeField]
      private bool clickOverBackgroundImageClosePanel;

      public bool ShowBackgroundImage
         => showBackgroundImage;

      public Color ColorBackgroundImage
         => colorBackgroundImage;

      public bool ClickOverBackgroundImageClosePanel
         => clickOverBackgroundImageClosePanel;
   }
}