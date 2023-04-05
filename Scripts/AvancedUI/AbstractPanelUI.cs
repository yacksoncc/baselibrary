﻿using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AvancedUI
{
   [RequireComponent(typeof(CanvasGroup))]
   public abstract class AbstractPanelUI<T> : MonoBehaviour where T : AbstractPanelUI<T>
   {
      private static T instance;

      public static T Instance
      {
         get
         {
            if(instance is not null)
               return instance;

            var tmpObjectsOfType = FindInstanceInScene();

            if(tmpObjectsOfType is null)
               Debug.LogError($"Panel with name: {typeof(T)} does exits in any scene, please setup.");
            else
               instance = tmpObjectsOfType;

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

      private IEnumerator couShowPanel;

      private IEnumerator couHiddePanel;

      private float factorTimeAnimation;

      [Header("Background panel")]
      [SerializeField]
      private SOPanelBackgroundConfiguration soPanelBackgroundConfiguration;

      private Image imageBackground;

      [Header("Scriptable Events")]
      [SerializeField, Tooltip("")]
      private ScriptableEvent seOnPanelShow;

      [SerializeField, Tooltip("Se ejecuta cuando el panel completa su ocultacion")]
      private ScriptableEvent seOnPanelHide;

      public ScriptableEvent SeOnPanelShow
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

      public ScriptableEvent SeOnPanelHide
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

      private RectTransform RectTransform
      {
         get
         {
            return rectTransform ??= GetComponent<RectTransform>();
         }
      }

      private void ShowPanel(bool argShowPanel = true, bool argDestroyObject = false)
      {
         canvasGroupAlpha ??= GetComponent<CanvasGroup>();

         if(argShowPanel)
         {
            if(couShowPanel is null)
            {
               gameObject.SetActive(true);

               AddImageBackground();

               if(couHiddePanel is not null)
                  StopCoroutine(couHiddePanel);

               couHiddePanel = null;
               couShowPanel = CouShowPanel();
               StartCoroutine(couShowPanel);
            }
            else
               Debug.LogError($"The panel {typeof(T)} is already is opened", this);
         }
         else
         {
            if(couHiddePanel is null)
            {
               if(couShowPanel is not null)
                  StopCoroutine(couShowPanel);

               couShowPanel = null;
               couHiddePanel = CouHiddePanel(argDestroyObject);
               StartCoroutine(couHiddePanel);
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
            tmpRectTransform.anchorMax = new float2(1f);
            tmpRectTransform.anchorMin = float2.zero;
            tmpRectTransform.offsetMax = float2.zero;
            tmpRectTransform.offsetMin = float2.zero;
            tmpRectTransform.localScale = new float3(1f);

            imageBackground.transform.SetSiblingIndex(transform.GetSiblingIndex());
            imageBackground.gameObject.layer = LayerMask.NameToLayer("UI");

            if(soPanelBackgroundConfiguration.ClickOverBackgroundImageClosePanel)
            {
               var tmpButton = imageBackground.gameObject.AddComponent<Button>();

               tmpButton.onClick.AddListener(() =>
               {
                  if(couShowPanel is not null)
                     ClosePanel();
               });
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
         var tmpActualTimeAnimation = factorTimeAnimation * soAnimationsCurvePanelUI.TiempoAparicion;

         while(tmpActualTimeAnimation <= soAnimationsCurvePanelUI.TiempoAparicion)
         {
            factorTimeAnimation = tmpActualTimeAnimation / soAnimationsCurvePanelUI.TiempoAparicion;
            RectTransform.localScale = new float3(1) * soAnimationsCurvePanelUI._animationCurveEscalaAparecer.Evaluate(factorTimeAnimation);
            canvasGroupAlpha.alpha = soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate(factorTimeAnimation);

            if(imageBackground)
               imageBackground.color = new Color(soPanelBackgroundConfiguration.ColorBackgroundImage[0], soPanelBackgroundConfiguration.ColorBackgroundImage[1], soPanelBackgroundConfiguration.ColorBackgroundImage[2], soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate(factorTimeAnimation) * soPanelBackgroundConfiguration.ColorBackgroundImage[3]);

            tmpActualTimeAnimation += Time.deltaTime;
            yield return null;
         }

         if(seOnPanelShow is not null)
            seOnPanelShow.ExecuteEvent();

         factorTimeAnimation = 1f;
      }

      private IEnumerator CouHiddePanel(bool argDestroyObject)
      {
         var tmpActualTimeAnimation = factorTimeAnimation * soAnimationsCurvePanelUI.TiempoOcultacion;

         while(tmpActualTimeAnimation >= 0)
         {
            factorTimeAnimation = tmpActualTimeAnimation / soAnimationsCurvePanelUI.TiempoOcultacion;
            RectTransform.localScale = new float3(1) * soAnimationsCurvePanelUI._animationCurveEscalaOcultar.Evaluate(1 - factorTimeAnimation);
            canvasGroupAlpha.alpha = soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar.Evaluate(1 - factorTimeAnimation);

            if(imageBackground)
               imageBackground.color = new Color(soPanelBackgroundConfiguration.ColorBackgroundImage[0], soPanelBackgroundConfiguration.ColorBackgroundImage[1], soPanelBackgroundConfiguration.ColorBackgroundImage[2], soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar.Evaluate(1 - factorTimeAnimation) * soPanelBackgroundConfiguration.ColorBackgroundImage[3]);

            tmpActualTimeAnimation -= Time.deltaTime;
            yield return null;
         }

         if(seOnPanelHide is not null)
            seOnPanelHide.ExecuteEvent();

         factorTimeAnimation = 0f;
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