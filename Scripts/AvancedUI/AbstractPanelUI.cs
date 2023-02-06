#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AvancedUI
{
   [RequireComponent(typeof(CanvasGroup))]
   public abstract class AbstractPanelUI<T> : MonoBehaviour where T : AbstractPanelUI<T>
   {
      protected static T instance;

      public static T Instance
      {
         get
         {
            if(instance != null)
               return instance;

            var tmpObjectsOfType = FindInstanceInScene();
            instance = tmpObjectsOfType != null? tmpObjectsOfType : (new GameObject(typeof(T).ToString())).AddComponent<T>();
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
      [SerializeField, Tooltip("Curva de animacion de la escala del panel cuando la ventana se esta mostrando")]
      private SOAnimationsCurvePanelUI soAnimationsCurvePanelUI;

      private CanvasGroup canvasGroupAlpha;

      private IEnumerator couShowPanel;

      private IEnumerator couHiddePanel;

      [Header("Image background panel")]
      [SerializeField]
      private SOImageBackgroundPanelConfiguration soImageBackgroundPanelConfiguration;

      private GameObject goBackgroundImage;

      /// <summary>
      /// Ejecutar evento cuando el panel aparece?
      /// </summary>
      [Header("Eventos"), Space(10)]
      [SerializeField, Tooltip("Ejecutar evento cuando el panel aparece?")]
      private bool callEventOnShow;

      /// <summary>
      /// Se ejecuta cuando el panel completa su aparicion
      /// </summary>
      [SerializeField, Tooltip("Se ejecuta cuando el panel completa su aparicion")]
      private ScriptableEvent seShowPanel;

      /// <summary>
      /// Ejecutar evento cuando el panel se oculta?
      /// </summary>
      [SerializeField, Tooltip("Ejecutar evento cuando el panel se oculta?")]
      private bool callEventHidde;

      /// <summary>
      /// Se ejecuta cuando el panel completa su ocultacion
      /// </summary>
      [SerializeField, Tooltip("Se ejecuta cuando el panel completa su ocultacion")]
      private ScriptableEvent seHiddePanel;

      public bool CallEventHidde
      {
         set
         {
            callEventHidde = value;
         }
      }

      public bool PanelOpened { get; set; }

      public ScriptableEvent SeShowPanel
      {
         set
         {
            seShowPanel = value;
         }
         get
         {
            return seShowPanel;
         }
      }

      public ScriptableEvent SeHiddePanel
      {
         set
         {
            seHiddePanel = value;
         }
         get
         {
            return seHiddePanel;
         }
      }

      /// <summary>
      /// Asigna el bool que permite que se pueda ejecutar el evento que notifica que debe ejecutarse el evento de ocultacion
      /// </summary>
      /// <param name="argEjecutarEvento">Ejecutar el evento de ocultacion? </param>
      public void CallEventHiddeMethod(bool argEjecutarEvento)
      {
         callEventHidde = argEjecutarEvento;
      }

      private void HiddePanel()
      {
         ShowPanel(false);
      }

      public virtual void ShowPanel(bool argShowPanel = true, bool argDestroyObject = false)
      {
         if(canvasGroupAlpha == null)
            canvasGroupAlpha = this.GetComponent<CanvasGroup>();

         if(argShowPanel)
         {
            if(couShowPanel == null)
            {
               if(gameObject.activeSelf)
                  return;

               gameObject.SetActive(true);

               if(soImageBackgroundPanelConfiguration)
                  if(soImageBackgroundPanelConfiguration.ShowBackgroundImage)
                  {
                     if(!goBackgroundImage)
                     {
                        goBackgroundImage = new GameObject("ventanaOscura");
                        goBackgroundImage.AddComponent<Image>();
                     }

                     goBackgroundImage.transform.SetParent(transform.parent);
                     var tmpImage = goBackgroundImage.GetComponent<Image>();
                     tmpImage.color = new Color(soImageBackgroundPanelConfiguration.ColorBackgroundImage[0], soImageBackgroundPanelConfiguration.ColorBackgroundImage[1], soImageBackgroundPanelConfiguration.ColorBackgroundImage[2], 0);
                     tmpImage.raycastTarget = true;

                     var tmpRectTransform = goBackgroundImage.GetComponent<RectTransform>();
                     tmpRectTransform.anchorMax = Vector2.one;
                     tmpRectTransform.anchorMin = Vector2.zero;
                     tmpRectTransform.offsetMax = Vector2.zero;
                     tmpRectTransform.offsetMin = Vector2.zero;
                     tmpRectTransform.localScale = Vector2.one;

                     goBackgroundImage.transform.SetSiblingIndex(transform.GetSiblingIndex());
                     goBackgroundImage.layer = LayerMask.NameToLayer("UI");

                     if(soImageBackgroundPanelConfiguration.ClickOverBackgroundImageClosePanel)
                     {
                        var tmpButton = goBackgroundImage.AddComponent<Button>();
                        tmpButton.onClick.AddListener(() =>
                        {
                           if(PanelOpened)
                              HiddePanel();
                        });
                     }
                  }

               if(couHiddePanel != null)
                  StopCoroutine(couHiddePanel);

               couHiddePanel = null;
               couShowPanel = CouShowPanel();
               StartCoroutine(couShowPanel);
            }
            else
               Debug.LogWarning("El panel ya esta ejecutando una animacion para aparecer", this);
         }
         else
         {
            if(!gameObject.activeSelf)
               return;

            if(couHiddePanel == null)
            {
               if(couShowPanel != null)
                  StopCoroutine(couShowPanel);

               couShowPanel = null;
               couHiddePanel = CouHiddePanel(argDestroyObject);
               StartCoroutine(couHiddePanel);
            }
            else
               Debug.LogWarning("El panel ya esta ejecutando una animacion para ocultarse", this);
         }
      }

      public void ClosePanel()
      {
         ShowPanel(false, false);
      }

      public void OpenPanel()
      {
         ShowPanel();
      }

      public void ShowPanel(bool argShowPanel)
      {
         ShowPanel(argShowPanel, false);
      }

      private IEnumerator CouShowPanel()
      {
         var tmpAnimationTime = 0f;
         var tmpRectTransform = GetComponent<RectTransform>();
         Image tmpBackgroundImage = null;

         if(goBackgroundImage)
            tmpBackgroundImage = goBackgroundImage.GetComponent<Image>();

         var tmpMaxTimeAnimationCurveScale = soAnimationsCurvePanelUI._animationCurveEscalaAparecer.keys[^1].time;
         var tmpMaxTimeAnimationCurveAlpha = soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.keys[^1].time;

         while(tmpAnimationTime <= soAnimationsCurvePanelUI.TiempoAparicion)
         {
            tmpRectTransform.localScale = Vector3.one * soAnimationsCurvePanelUI._animationCurveEscalaAparecer.Evaluate((tmpAnimationTime * tmpMaxTimeAnimationCurveScale) / soAnimationsCurvePanelUI.TiempoAparicion);
            canvasGroupAlpha.alpha = soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate((tmpAnimationTime * tmpMaxTimeAnimationCurveAlpha) / soAnimationsCurvePanelUI.TiempoAparicion);

            if(tmpBackgroundImage)
               tmpBackgroundImage.color = new Color(soImageBackgroundPanelConfiguration.ColorBackgroundImage[0], soImageBackgroundPanelConfiguration.ColorBackgroundImage[1], soImageBackgroundPanelConfiguration.ColorBackgroundImage[2], soAnimationsCurvePanelUI._animationCurveTransparenciaAparecer.Evaluate((tmpAnimationTime * tmpMaxTimeAnimationCurveAlpha) / soAnimationsCurvePanelUI.TiempoAparicion) * soImageBackgroundPanelConfiguration.ColorBackgroundImage[3]);

            tmpAnimationTime += Time.deltaTime;
            yield return null;
         }

         if(callEventOnShow)
            seShowPanel.ExecuteEvent();

         couShowPanel = null;
         PanelOpened = true;
      }

      private IEnumerator CouHiddePanel(bool argDestroyObject)
      {
         var tmpTiempoAnimacion = 0f;
         var tmpRectTransform = GetComponent<RectTransform>();
         Image tmpImagenFondo = null;

         if(goBackgroundImage)
            tmpImagenFondo = goBackgroundImage.GetComponent<Image>();

         var tmpMaxTimeAnimationCurveScale = soAnimationsCurvePanelUI._animationCurveEscalaOcultar.keys[^1].time;
         var tmpMaxTimeAnimationCurveAlpha = soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar.keys[^1].time;

         while(tmpTiempoAnimacion <= soAnimationsCurvePanelUI.TiempoOcultacion)
         {
            tmpRectTransform.localScale = Vector3.one * soAnimationsCurvePanelUI._animationCurveEscalaOcultar.Evaluate((tmpTiempoAnimacion * tmpMaxTimeAnimationCurveScale) / soAnimationsCurvePanelUI.TiempoOcultacion);
            canvasGroupAlpha.alpha = soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar.Evaluate((tmpTiempoAnimacion * tmpMaxTimeAnimationCurveAlpha) / soAnimationsCurvePanelUI.TiempoOcultacion);

            if(tmpImagenFondo)
               tmpImagenFondo.color = new Color(soImageBackgroundPanelConfiguration.ColorBackgroundImage[0], soImageBackgroundPanelConfiguration.ColorBackgroundImage[1], soImageBackgroundPanelConfiguration.ColorBackgroundImage[2], soAnimationsCurvePanelUI._animationCurveTransparenciaOcultar.Evaluate((tmpTiempoAnimacion * tmpMaxTimeAnimationCurveAlpha) / soAnimationsCurvePanelUI.TiempoOcultacion) * soImageBackgroundPanelConfiguration.ColorBackgroundImage[3]);

            tmpTiempoAnimacion += Time.deltaTime;
            yield return null;
         }

         if(callEventHidde)
            seHiddePanel.ExecuteEvent();

         couHiddePanel = null;
         PanelOpened = false;
         gameObject.SetActive(false);

         if(soImageBackgroundPanelConfiguration.ShowBackgroundImage)
            if(goBackgroundImage)
               Destroy(goBackgroundImage);

         if(argDestroyObject)
            DestroyImmediate(gameObject);
      }
   }
}