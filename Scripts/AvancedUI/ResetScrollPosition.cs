#pragma warning disable
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AvancedUI
{
   public class ResetScrollPosition : MonoBehaviour
   {
      [SerializeField]
      private ScrollRect refScrollRect;

      [SerializeField]
      private RectTransform refRectTransform;

      [SerializeField]
      private bool resetInOnEnable;

      [SerializeField]
      private Vector2 positionScrollRect;

      private void OnEnable()
      {
         if(resetInOnEnable)
            StartCoroutine(CouResetScrollBarPosition());
      }

      private IEnumerator CouResetScrollBarPosition()
      {
         yield return null;
         LayoutRebuilder.ForceRebuildLayoutImmediate(refRectTransform);
         yield return null;
         refScrollRect.normalizedPosition = positionScrollRect;
      }
   }
}