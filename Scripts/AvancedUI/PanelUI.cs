using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AvancedUI
{
   [RequireComponent(typeof(CanvasGroup))]
   public abstract class PanelUI<T> : MonoBehaviour where T : PanelUI<T>
   {
      private Vector3 initScale;

      private static T instance;

      public static T Instance
      {
         get
         {
            if(instance == null)
            {
               var tmpObjectsOfType = FindInstanceInScene();

               if(tmpObjectsOfType == null)
                  Debug.LogError($"Panel with name: {typeof(T)} does exits in any scene, please setup.");
               else
                  instance = tmpObjectsOfType;
            }

            return instance;
         }
      }

      private static T FindInstanceInScene()
      {
         var tmpObjectsInScene = new List<T>();

         foreach(var tmpObjectFinded in Resources.FindObjectsOfTypeAll<T>())
         {
            if(tmpObjectFinded.gameObject.scene == default(Scene))
               continue;

            tmpObjectsInScene.Add(tmpObjectFinded.GetComponent<T>());
         }

         return tmpObjectsInScene.Count > 0? tmpObjectsInScene[0] : null;
      }

      [Header("Animación panel")]
      [SerializeField, Tooltip("Animations curve that control how the panel looks on screen. Create a new SO with curves in rightClick/Create/PanelUI/SOAnimationsCurvePanelUI")]
      private SOAnimationsCurvePanelUI soAnimationsCurvePanelUI;

      private CanvasGroup canvasGroupAlpha;

      private RectTransform rectTransform;

      private float factorTimeAnimation;

      [Header("Background panel")]
      [SerializeField]
      private SOPanelBackgroundConfiguration soPanelBackgroundConfiguration;

      private Image imageBackground;

      [Header("Scriptable Events")]
      [SerializeField, Tooltip("")]
      private ScriptableEventEmpty seOnPanelShow;

      [SerializeField, Tooltip("Se ejecuta cuando el panel completa su ocultacion")]
      private ScriptableEventEmpty seOnPanelHide;

      public ScriptableEventEmpty SeOnPanelShow
      {
         set
         {
            seOnPanelShow = value;
         }
         get
         {
            return seOnPanelShow;
         }
      }

      public ScriptableEventEmpty SeOnPanelHide
      {
         set
         {
            seOnPanelHide = value;
         }
         get
         {
            return seOnPanelHide;
         }
      }

      protected RectTransform RectTransform
      {
         get
         {
            return rectTransform ??= GetComponent<RectTransform>();
         }
      }

      public bool IsOpen { get; set; }

      public bool IsClosing { get; set; }

      public bool IsOpening { get; set; }

      private void ShowPanel(bool argShowPanel = true, bool argDestroyObject = false)
      {
         if(initScale.magnitude == 0)
            initScale = RectTransform.localScale;

         canvasGroupAlpha ??= GetComponent<CanvasGroup>();

         if(argShowPanel)
         {
            if(!IsOpen && !IsOpening)
            {
               gameObject.SetActive(true);
               AddImageBackground();
               IsClosing = false;
               StopAllCoroutines();
               StartCoroutine(CouShowPanel());
            }
            else
               Debug.LogError($"The panel {typeof(T)} is already is opened", this);
         }
         else
         {
            if(IsOpen && !IsClosing)
            {
               IsOpening = false;
               StopAllCoroutines();
               StartCoroutine(CouHiddePanel(argDestroyObject));
            }
            else
               Debug.LogError($"The panel {typeof(T)} is already closed", this);
         }
      }

      private void AddImageBackground()
      {
         if(imageBackground)
            return;

         if(soPanelBackgroundConfiguration && soPanelBackgroundConfiguration.ShowBackgroundImage)
         {
            imageBackground ??= (new GameObject("BackgroundPanel")).AddComponent<Image>();
            imageBackground.transform.SetParent(transform.parent);
            imageBackground.color = new Color(soPanelBackgroundConfiguration.ColorBackgroundImage[0], soPanelBackgroundConfiguration.ColorBackgroundImage[1], soPanelBackgroundConfiguration.ColorBackgroundImage[2], 0);
            imageBackground.raycastTarget = true;

            var tmpRectTransform = imageBackground.GetComponent<RectTransform>();
            tmpRectTransform.anchorMax = Vector2.one;
            tmpRectTransform.anchorMin = Vector2.zero;
            tmpRectTransform.offsetMax = Vector2.zero;
            tmpRectTransform.offsetMin = Vector2.zero;
            tmpRectTransform.localScale = Vector3.one;

            imageBackground.transform.SetSiblingIndex(transform.GetSiblingIndex());
            imageBackground.gameObject.layer = LayerMask.NameToLayer("UI");

            if(soPanelBackgroundConfiguration.ClickOverBackgroundImageClosePanel)
            {
               var tmpButton = imageBackground.gameObject.AddComponent<Button>();
               tmpButton.onClick.AddListener(ClosePanel);
            }
         }
      }

      public void ClosePanel()
      {
         ShowPanel(false);
      }

      public void ClosePanelAndDestroyIt()
      {
         ShowPanel(false, true);
      }

      public void OpenPanel()
      {
         ShowPanel();
      }

      private IEnumerator CouShowPanel()
      {
         IsOpening = true;
         var tmpActualTimeAnimation = factorTimeAnimation * soAnimationsCurvePanelUI.TiempoAparicion;

         while(tmpActualTimeAnimation <= soAnimationsCurvePanelUI.TiempoAparicion)
         {
            factorTimeAnimation = tmpActualTimeAnimation / soAnimationsCurvePanelUI.TiempoAparicion;
            RectTransform.localScale = initScale * soAnimationsCurvePanelUI._animationCurveEscalaAparecer.Evaluate(factorTimeAnimation);
            canvasGroupAlpha.alpha = soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate(factorTimeAnimation);

            if(imageBackground)
               imageBackground.color = new Color(soPanelBackgroundConfiguration.ColorBackgroundImage[0], soPanelBackgroundConfiguration.ColorBackgroundImage[1], soPanelBackgroundConfiguration.ColorBackgroundImage[2], soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate(factorTimeAnimation) * soPanelBackgroundConfiguration.ColorBackgroundImage[3]);

            tmpActualTimeAnimation += Time.deltaTime;
            yield return null;
         }

         factorTimeAnimation = 1f;
         RectTransform.localScale = initScale * soAnimationsCurvePanelUI._animationCurveEscalaAparecer.Evaluate(factorTimeAnimation);
         canvasGroupAlpha.alpha = soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate(factorTimeAnimation);

         if(imageBackground)
            imageBackground.color = new Color(soPanelBackgroundConfiguration.ColorBackgroundImage[0], soPanelBackgroundConfiguration.ColorBackgroundImage[1], soPanelBackgroundConfiguration.ColorBackgroundImage[2], soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate(factorTimeAnimation) * soPanelBackgroundConfiguration.ColorBackgroundImage[3]);

         if(seOnPanelShow)
            seOnPanelShow.ExecuteEvent();

         IsOpening = false;
         IsOpen = true;
      }

      private IEnumerator CouHiddePanel(bool argDestroyObject)
      {
         IsClosing = true;
         var tmpActualTimeAnimation = factorTimeAnimation * soAnimationsCurvePanelUI.TiempoOcultacion;

         while(tmpActualTimeAnimation >= 0)
         {
            factorTimeAnimation = tmpActualTimeAnimation / soAnimationsCurvePanelUI.TiempoOcultacion;
            RectTransform.localScale = initScale * soAnimationsCurvePanelUI._animationCurveEscalaOcultar.Evaluate(1 - factorTimeAnimation);
            canvasGroupAlpha.alpha = soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar.Evaluate(1 - factorTimeAnimation);

            if(imageBackground)
               imageBackground.color = new Color(soPanelBackgroundConfiguration.ColorBackgroundImage[0], soPanelBackgroundConfiguration.ColorBackgroundImage[1], soPanelBackgroundConfiguration.ColorBackgroundImage[2], soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar.Evaluate(1 - factorTimeAnimation) * soPanelBackgroundConfiguration.ColorBackgroundImage[3]);

            tmpActualTimeAnimation -= Time.deltaTime;
            yield return null;
         }

         if(seOnPanelHide)
            seOnPanelHide.ExecuteEvent();

         factorTimeAnimation = 0f;
         IsOpen = false;
         IsClosing = false;
         gameObject.SetActive(false);

         if(imageBackground)
         {
            Destroy(imageBackground.gameObject);
            imageBackground = null;
         }

         if(argDestroyObject)
            DestroyImmediate(gameObject);
      }
   }
}