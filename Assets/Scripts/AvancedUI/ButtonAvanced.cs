#pragma warning disable
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Extensions;

namespace AvancedUI
{
   public class ButtonAvanced : Selectable
   {
      public UnityEvent OnButtonTab;

      public UnityEvent OnButtonDown;

      public UnityEventFloat OnButtonStayPressedWithTime;

      public UnityEventFloat OnButtonUpWithTimePressed;

      [SerializeField]
      private float tabThreshold = 0.2f;

      private float actualTimeTresholdTab;

      private bool buttonIsPressed;

      private void Update()
      {
         if(actualTimeTresholdTab > tabThreshold && buttonIsPressed)
            OnButtonStayPressedWithTime.Invoke(actualTimeTresholdTab);

         actualTimeTresholdTab += Time.deltaTime;
      }

      protected override void OnDisable()
      {
         base.OnDisable();
         buttonIsPressed = false;
      }

      public override void OnPointerDown(PointerEventData eventData)
      {
         base.OnPointerDown(eventData);

         if(!IsInteractable())
            return;

         OnButtonDown.Invoke();
         actualTimeTresholdTab = 0;
         buttonIsPressed = true;
      }

      public override void OnPointerUp(PointerEventData eventData)
      {
         base.OnPointerUp(eventData);

         if(!IsInteractable())
            return;

         if(actualTimeTresholdTab <= tabThreshold)
            OnButtonTab.Invoke();

         OnButtonUpWithTimePressed.Invoke(actualTimeTresholdTab);
         buttonIsPressed = false;
      }
   }
}