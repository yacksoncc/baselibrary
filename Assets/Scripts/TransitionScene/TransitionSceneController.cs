using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TransitionScene
{
   public class TransitionSceneController : MonoBehaviour
   {
      [SerializeField]
      private Image imageTransicionScene;

      [SerializeField]
      private Color colorFadeIn;

      [SerializeField]
      private Color colorFadeOut;

      [SerializeField]
      private float timeAnimation;

      [SerializeField]
      private AnimationCurve acFade;

      [SerializeField]
      private bool playAnimationAwake;

      private float actualTimeAnimationFade;

      [SerializeField]
      private bool automaticTransicion = true;

      [SerializeField]
      private float transicionInSeconds;

      [SerializeField]
      private string transicionToSceneName;

      [SerializeField]
      private bool loadSceneAdditive;

      public static TransitionSceneController Instance { get; private set; }

      private void Awake()
      {
         if(Instance == null)
         {
            Instance = this;
            actualTimeAnimationFade = timeAnimation;

            if(playAnimationAwake)
               FadeIn();
         }
      }

      public void FadeIn()
      {
         StopAllCoroutines();
         StartCoroutine(CouFadeIn());
      }

      public void FadeOut()
      {
         StopAllCoroutines();
         StartCoroutine(CouFadeOut());
      }

      private IEnumerator CouFadeIn()
      {
         imageTransicionScene.color = new Color(colorFadeIn[0], colorFadeIn[1], colorFadeIn[2], acFade.Evaluate(actualTimeAnimationFade / timeAnimation));

         while(actualTimeAnimationFade > 0)
         {
            actualTimeAnimationFade -= Time.deltaTime;
            var tmpFactor = acFade.Evaluate(actualTimeAnimationFade / timeAnimation);
            imageTransicionScene.color = new Color(colorFadeIn[0], colorFadeIn[1], colorFadeIn[2], tmpFactor);
            yield return null;
         }

         imageTransicionScene.color = new Color(colorFadeIn[0], colorFadeIn[1], colorFadeIn[2], acFade.Evaluate(actualTimeAnimationFade / timeAnimation));

         if(automaticTransicion)
         {
            yield return new WaitForSeconds(transicionInSeconds);
            yield return StartCoroutine(CouFadeOut());
            SceneManager.LoadScene(transicionToSceneName, loadSceneAdditive? LoadSceneMode.Additive : LoadSceneMode.Single);
         }
      }

      private IEnumerator CouFadeOut()
      {
         imageTransicionScene.color = new Color(colorFadeOut[0], colorFadeOut[1], colorFadeOut[2], acFade.Evaluate(actualTimeAnimationFade / timeAnimation));

         while(actualTimeAnimationFade < timeAnimation)
         {
            actualTimeAnimationFade += Time.deltaTime;
            var tmpFactor = acFade.Evaluate(actualTimeAnimationFade / timeAnimation);
            imageTransicionScene.color = new Color(colorFadeOut[0], colorFadeOut[1], colorFadeOut[2], tmpFactor);
            yield return null;
         }

         imageTransicionScene.color = new Color(colorFadeOut[0], colorFadeOut[1], colorFadeOut[2], acFade.Evaluate(actualTimeAnimationFade / timeAnimation));
      }
   }
}