#pragma warning disable
using Extensions;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AvancedUI
{
   public class ToggleAvanced : Toggle, IPointerDownHandler
   {
      public UnityEvent OnTogglePointerDown;

      public UnityEvent ToggleOn;

      public UnityEvent ToggleOff;

      public UnityEventInt ToggleOnSiblingIndex = new UnityEventInt();
      

      protected override void Awake()
      {
         base.Awake();
         onValueChanged.AddListener(NotifyToggleState);
      }

      private void NotifyToggleState(bool argState)
      {
         if(argState)
         {
            ToggleOn.Invoke();
            ToggleOnSiblingIndex.Invoke(transform.GetSiblingIndex());
         }
         else
            ToggleOff.Invoke();
      }

      public void OnPointerDown(PointerEventData eventData)
      {
         if(eventData.button != PointerEventData.InputButton.Left)
            return;

         OnTogglePointerDown.Invoke();
      }
   }
}