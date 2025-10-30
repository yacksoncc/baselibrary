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
      protected static T instance;

      public static T Instance
      {
         get
         {
            if(instance == null)
            {
               var tmpFoundInstance = FindInstanceInScene();

               if(tmpFoundInstance == null)
                  Debug.LogWarning($"Panel UI with name: {typeof(T)} does not exist in scene, please setup.");
               else
                  instance = tmpFoundInstance;
            }

            return instance;
         }
      }

      private static T FindInstanceInScene()
      {
         var tmpInstancesInScene = new List<T>();

         foreach(var tmpFoundObject in Resources.FindObjectsOfTypeAll<T>())
         {
            if(tmpFoundObject.gameObject.scene == default)
               continue;

            tmpInstancesInScene.Add(tmpFoundObject.GetComponent<T>());
         }

         return tmpInstancesInScene.Count > 0? tmpInstancesInScene[0] : null;
      }

      [Header("Panel Animation")]
      [SerializeField, Tooltip("Animation curves that control how the panel appears/disappears on screen")]
      private SOAnimationsCurvePanelUI soAnimationsCurvePanelUI;

      private Vector3 initialScale;
      
      private CanvasGroup canvasGroup;
      
      private RectTransform rectTransform;
      
      private float animationTimeProgress;
      
      private IEnumerator showPanelCoroutine;
      
      private IEnumerator hidePanelCoroutine;

      [Header("Background Panel")]
      [SerializeField]
      private SOPanelBackgroundConfiguration soPanelBackgroundConfiguration;

      private Image backgroundImage;

      [Header("Scriptable Events")]
      [SerializeField, Tooltip("Executed when panel completes showing")]
      private ScriptableEventEmpty seOnPanelShow;

      [SerializeField, Tooltip("Executed when panel completes hiding")]
      private ScriptableEventEmpty seOnPanelHide;

      public ScriptableEventEmpty OnPanelShowEvent
      {
         set => seOnPanelShow = value;
         get => seOnPanelShow;
      }

      public ScriptableEventEmpty OnPanelHideEvent
      {
         set => seOnPanelHide = value;
         get => seOnPanelHide;
      }

      protected RectTransform RectTransform
         => rectTransform ??= GetComponent<RectTransform>();

      public bool IsOpen { get; private set; }
      public bool IsClosing { get; private set; }
      public bool IsOpening { get; private set; }

      private void InitializeComponents()
      {
         if(initialScale.magnitude == 0)
            initialScale = RectTransform.localScale;

         canvasGroup ??= GetComponent<CanvasGroup>();
      }

      private void ExecuteAnimation(bool argShow, bool argDestroyAfterHide = false)
      {
         InitializeComponents();
         StopAllPanelCoroutines();

         if(argShow)
            StartShowAnimation();
         else
            StartHideAnimation(argDestroyAfterHide);
      }

      private void StartShowAnimation()
      {
         gameObject.SetActive(true);
         CreateBackgroundIfNeeded();

         IsClosing = false;
         IsOpening = true;

         showPanelCoroutine = CouShowPanel();
         StartCoroutine(showPanelCoroutine);
      }

      private void StartHideAnimation(bool argDestroyAfterHide)
      {
         IsOpening = false;
         IsClosing = true;

         hidePanelCoroutine = CouHidePanel(argDestroyAfterHide);
         StartCoroutine(hidePanelCoroutine);
      }

      private void StopAllPanelCoroutines()
      {
         if(showPanelCoroutine != null)
            StopCoroutine(showPanelCoroutine);

         if(hidePanelCoroutine != null)
            StopCoroutine(hidePanelCoroutine);
      }

      private void CreateBackgroundIfNeeded()
      {
         if(backgroundImage || !ShouldShowBackground())
            return;

         CreateBackgroundImage();
         ConfigureBackgroundTransform();
         SetupBackgroundInteraction();
      }

      private bool ShouldShowBackground()
      {
         return soPanelBackgroundConfiguration && soPanelBackgroundConfiguration.ShowBackgroundImage;
      }

      private void CreateBackgroundImage()
      {
         var tmpBackgroundObject = new GameObject("BackgroundPanel");
         backgroundImage = tmpBackgroundObject.AddComponent<Image>();
         backgroundImage.transform.SetParent(transform.parent);
         backgroundImage.color = GetBackgroundColorWithAlpha(0f);
         backgroundImage.raycastTarget = true;
         backgroundImage.gameObject.layer = LayerMask.NameToLayer("UI");
         backgroundImage.transform.SetSiblingIndex(transform.GetSiblingIndex());
      }

      private void ConfigureBackgroundTransform()
      {
         var tmpRectTransform = backgroundImage.GetComponent<RectTransform>();
         tmpRectTransform.anchorMax = Vector2.one;
         tmpRectTransform.anchorMin = Vector2.zero;
         tmpRectTransform.offsetMax = Vector2.zero;
         tmpRectTransform.offsetMin = Vector2.zero;
         tmpRectTransform.localScale = Vector3.one;
      }

      private void SetupBackgroundInteraction()
      {
         if(soPanelBackgroundConfiguration.ClickOverBackgroundImageClosePanel)
         {
            var tmpButton = backgroundImage.gameObject.AddComponent<Button>();
            tmpButton.onClick.AddListener(ClosePanel);
         }
      }

      private Color GetBackgroundColorWithAlpha(float argAlpha)
      {
         var tmpColorArray = soPanelBackgroundConfiguration.ColorBackgroundImage;
         return new Color(tmpColorArray[0], tmpColorArray[1], tmpColorArray[2], argAlpha);
      }

      private void UpdatePanelVisuals(float argAnimationFactor, bool argIsShowing)
      {
         var tmpScaleCurve = argIsShowing? soAnimationsCurvePanelUI._animationCurveEscalaAparecer : soAnimationsCurvePanelUI._animationCurveEscalaOcultar;
         var tmpAlphaCurve = argIsShowing? soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer : soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar;

         var tmpEvaluationValue = argIsShowing? argAnimationFactor : 1f - argAnimationFactor;

         RectTransform.localScale = initialScale * tmpScaleCurve.Evaluate(tmpEvaluationValue);
         canvasGroup.alpha = tmpAlphaCurve.Evaluate(tmpEvaluationValue);

         if(backgroundImage)
         {
            var tmpBackgroundAlpha = tmpAlphaCurve.Evaluate(tmpEvaluationValue) * soPanelBackgroundConfiguration.ColorBackgroundImage[3];
            backgroundImage.color = GetBackgroundColorWithAlpha(tmpBackgroundAlpha);
         }
      }

      private void DestroyBackground()
      {
         if(backgroundImage)
         {
            Destroy(backgroundImage.gameObject);
            backgroundImage = null;
         }
      }

      public void ClosePanel()
      {
         ExecuteAnimation(false);
      }

      public void ClosePanelImmediately()
      {
         ResetPanelState();

         animationTimeProgress = 0f;
         UpdatePanelVisuals(0f, false);

         gameObject.SetActive(false);
         DestroyBackground();
      }

      public void ClosePanelAndDestroy()
      {
         ExecuteAnimation(false, true);
      }

      public void OpenPanel()
      {
         ExecuteAnimation(true);
      }

      public void ForceOpenPanelFromZero()
      {
         ResetPanelState();
         InitializeComponents();
         StopAllPanelCoroutines();
         UpdatePanelVisuals(0f, false);

         if(backgroundImage)
            backgroundImage.color = GetBackgroundColorWithAlpha(0f);

         StartShowAnimation();
      }

      private void ResetPanelState()
      {
         IsOpen = false;
         IsOpening = false;
         IsClosing = false;
         animationTimeProgress = 0f;
      }

      private IEnumerator CouShowPanel()
      {
         IsOpen = true;

         var tmpCurrentAnimationTime = animationTimeProgress * soAnimationsCurvePanelUI.TiempoAparicion;
         var tmpTotalAnimationTime = soAnimationsCurvePanelUI.TiempoAparicion;

         while(tmpCurrentAnimationTime <= tmpTotalAnimationTime)
         {
            animationTimeProgress = tmpCurrentAnimationTime / tmpTotalAnimationTime;
            UpdatePanelVisuals(animationTimeProgress, true);

            tmpCurrentAnimationTime += Time.deltaTime;
            yield return null;
         }

         // Ensure final state
         animationTimeProgress = 1f;
         UpdatePanelVisuals(animationTimeProgress, true);

         seOnPanelShow?.ExecuteEvent();
         IsOpening = false;
      }

      private IEnumerator CouHidePanel(bool argDestroyObject)
      {
         if(animationTimeProgress == 0f)
            animationTimeProgress = 1f;

         var tmpCurrentAnimationTime = animationTimeProgress * soAnimationsCurvePanelUI.TiempoOcultacion;

         while(tmpCurrentAnimationTime >= 0f)
         {
            animationTimeProgress = tmpCurrentAnimationTime / soAnimationsCurvePanelUI.TiempoOcultacion;
            UpdatePanelVisuals(animationTimeProgress, false);

            tmpCurrentAnimationTime -= Time.deltaTime;
            yield return null;
         }

         seOnPanelHide?.ExecuteEvent();

         ResetPanelState();
         gameObject.SetActive(false);
         DestroyBackground();

         if(argDestroyObject)
            DestroyImmediate(gameObject);
      }

      public static bool PanelExits()
      {
         return instance != null;
      }
   }
}