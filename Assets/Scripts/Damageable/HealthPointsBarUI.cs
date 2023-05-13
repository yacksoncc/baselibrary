using PoolSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Damageable
{
   public class HealthPointsBarUI : MonoBehaviour
   {
      [SerializeField]
      private Image refImageHealthBar;

      [SerializeField]
      private Image refImageShieldBar;

      [SerializeField]
      private bool animateBar;

      private float actualHealth;

      private float actualShield;

      [SerializeField]
      private float timeAnimateShieldBar = 5;

      [SerializeField]
      private float timeAnimateHealthBar = 5;

      [SerializeField]
      private bool alwaysShowOnCanvas;

      private PooleableObject refPooleableObject;

      private bool ownerIsAlive;

      public bool OwnerIsAlive
      {
         get => ownerIsAlive;
         set
         {
            ownerIsAlive = value;

            if(!ownerIsAlive && refPooleableObject)
               refPooleableObject.DestroyPooleableObject();
         } 
      }

      private void Awake()
      {
         refPooleableObject = GetComponent<PooleableObject>();
      }

      private void Update()
      {
         if(animateBar)
         {
            refImageHealthBar.fillAmount = Mathf.Lerp(refImageHealthBar.fillAmount, actualHealth, timeAnimateHealthBar * Time.deltaTime);
            refImageShieldBar.fillAmount = Mathf.Lerp(refImageShieldBar.fillAmount, actualShield, timeAnimateShieldBar * Time.deltaTime);
         }
         else
         {
            refImageHealthBar.fillAmount = actualHealth;
            refImageShieldBar.fillAmount = actualShield;
         }
      }

      public void UpdateUIHealthAndShield(float argActualShieldFactor, float argActualHealthFactor)
      {
         transform.localScale = Vector3.one;
         gameObject.SetActive(argActualShieldFactor < 1f || alwaysShowOnCanvas);
         actualHealth = argActualHealthFactor;
         actualShield = argActualShieldFactor;
      }

      public void UpdateUIHealthPoints(float argActualHealthFactor)
      {
         gameObject.SetActive(argActualHealthFactor < 1f || alwaysShowOnCanvas);
         actualHealth = argActualHealthFactor;
      }

      public void UpdateUIShieldPoints(float argActualShieldFactor)
      {
         actualShield = argActualShieldFactor;
      }

      public void SetPositionOnCanvas(Vector3 argWorldToScreenPoint)
      {
         (transform as RectTransform).position = argWorldToScreenPoint;
      }
   }
}