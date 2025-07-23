using System.Collections;
using AvancedUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoxMessage
{
   public delegate void delegateOnBoxMessageButtonAccept();

   public delegate void delegateOnBoxMessageButtonCancel();

   public abstract class BoxMessage : PanelUI<BoxMessage>
   {
      private TextMeshProUGUI textMessage;

      protected Button buttonAccept;

      protected Button buttonCancel;

      private float timeForDestroy;

      private TextMeshProUGUI textButtonAccept;

      private TextMeshProUGUI textButtonCancel;

      private IEnumerator couInstanceDestroy;

      public float HiddeWithDelaySeconds
      {
         set
         {
            timeForDestroy = value;

            if(timeForDestroy == -1)
               return;

            couInstanceDestroy = couDestroy(timeForDestroy);
            StartCoroutine(couInstanceDestroy);
         }
      }

      public string TextMessage
      {
         set
         {
            if(textMessage == null)
               textMessage = transform.Find("ImageFondoTexto").GetChild(0).GetComponent<TextMeshProUGUI>();

            textMessage.text = value;
         }
      }

      public string TextButtonAccept
      {
         set
         {
            textButtonAccept.text = value;
         }
      }

      public string TextButtonCancel
      {
         set
         {
            textButtonCancel.text = value;
         }
      }

      public delegateOnBoxMessageButtonAccept dltOnButtonAccept;

      public delegateOnBoxMessageButtonCancel dltOnButtonCancel;

      protected void Awake()
      {
         var tempButtonAccept = transform.Find("ButtonAccept");

         if(tempButtonAccept != null)
         {
            buttonAccept = tempButtonAccept.GetComponent<Button>();
            buttonAccept.onClick.AddListener(OnButtonAccept);
            textButtonAccept = tempButtonAccept.GetChild(0).GetComponent<TextMeshProUGUI>();
         }

         var tempButtonCancel = transform.Find("ButtonCancel");

         if(tempButtonCancel != null)
         {
            buttonCancel = tempButtonCancel.GetComponent<Button>();
            buttonCancel.onClick.AddListener(OnButtonCancel);
            textButtonCancel = tempButtonCancel.GetChild(0).GetComponent<TextMeshProUGUI>();
         }
      }

      protected virtual void OnButtonAccept()
      {
         if(dltOnButtonAccept != null)
            dltOnButtonAccept();
      }

      protected virtual void OnButtonCancel()
      {
         if(dltOnButtonCancel != null)
            dltOnButtonCancel();
      }

      /// <summary>
      /// Add a method that is executed when button Accept is pressed
      /// </summary>
      /// <param name="argDelegate">function signature</param>
      public void AddDelegateButtonAccept(delegateOnBoxMessageButtonAccept argDelegate)
      {
         dltOnButtonAccept += argDelegate;
      }

      /// <summary>
      /// Add a method that is Execute when button Cancel is pressed
      /// </summary>
      /// <param name="argDelegate">function signature</param>
      public void AddDelegateButtonCancel(delegateOnBoxMessageButtonCancel argDelegate)
      {
         dltOnButtonCancel += argDelegate;
      }

      private IEnumerator couDestroy(float argTimeForDestroy)
      {
         yield return new WaitForSeconds(argTimeForDestroy);
         OnButtonAccept();
         ClosePanelAndDestroy();
      }
   }
}