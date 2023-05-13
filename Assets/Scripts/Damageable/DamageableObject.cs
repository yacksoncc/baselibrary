using Patterns.Observer;
using UnityEngine;
using UnityEngine.Events;

namespace Damageable
{
   public class DamageableObject : Subject
   {
      [Header("Health")]
      [SerializeField]
      private float maxHealthPoints;

      private float actualHealthPoints;

      [Header("Shield")]
      [SerializeField]
      private bool hasShieldPoints;

      [SerializeField]
      private float maxShieldPoints;

      private float actualShieldPoints;

      [SerializeField]
      private bool canRegenerateShieldPoints;

      [SerializeField]
      private float quantityShieldPointsRegeneratedPerSecond = 10f;

      [SerializeField]
      private float timeWaitForRegenerateShieldPointsAgain = 2f;

      private float actualTimeWithoutRegenerateShieldPoints;

      public UnityEvent OnDie;

      public float ActualHealthPointsNormalized
      {
         get
         {
            return actualHealthPoints / maxHealthPoints;
         }
      }

      public float ActualShieldPointsNormalized
      {
         get
         {
            return actualShieldPoints / maxShieldPoints;
         }
      }

      public bool Die { get; private set; }

      private void OnEnable()
      {
         ResetHealthPointsToMaxValue();
      }

      private void Update()
      {
         RegenerateShieldPoints();
      }

      private void RegenerateShieldPoints()
      {
         if(Die)
            return;

         if(hasShieldPoints && canRegenerateShieldPoints)
         {
            actualTimeWithoutRegenerateShieldPoints -= Time.deltaTime;

            if(actualTimeWithoutRegenerateShieldPoints < 0)
            {
               actualShieldPoints += quantityShieldPointsRegeneratedPerSecond * Time.deltaTime;
               actualShieldPoints = Mathf.Clamp(actualShieldPoints, 0f, maxShieldPoints);
               NotifyToObservers(DamageableUnitNotyfications.UpdateHealthPoints);
            }
         }
      }

      private void ResetHealthPointsToMaxValue()
      {
         Die = false;

         NotifyToObservers(DamageableUnitNotyfications.ResetHealthPointsToMaxValue);

         actualHealthPoints = maxHealthPoints;
         NotifyToObservers(DamageableUnitNotyfications.UpdateHealthPoints);

         actualTimeWithoutRegenerateShieldPoints = timeWaitForRegenerateShieldPointsAgain;

         if(hasShieldPoints)
         {
            actualShieldPoints = maxShieldPoints;
            NotifyToObservers(DamageableUnitNotyfications.UpdateShieldPoints);
         }
      }

      public void RemoveHealthPoints(float argHealthPointsToRemove)
      {
         if(Die)
            return;

         if(hasShieldPoints)
         {
            actualTimeWithoutRegenerateShieldPoints = timeWaitForRegenerateShieldPointsAgain;

            if(actualShieldPoints > 0)
            {
               actualShieldPoints -= argHealthPointsToRemove;

               if(actualShieldPoints < 0)
               {
                  actualHealthPoints -= Mathf.Abs(actualShieldPoints);
                  actualShieldPoints = 0;
               }

               NotifyToObservers(DamageableUnitNotyfications.UpdateHealthPoints);
               NotifyToObservers(DamageableUnitNotyfications.UpdateShieldPoints);
            }
            else
            {
               actualHealthPoints -= argHealthPointsToRemove;
               NotifyToObservers(DamageableUnitNotyfications.UpdateHealthPoints);
            }
         }
         else
         {
            actualHealthPoints -= argHealthPointsToRemove;
            NotifyToObservers(DamageableUnitNotyfications.UpdateHealthPoints);
         }

         actualHealthPoints = Mathf.Clamp(actualHealthPoints, 0, int.MaxValue);
         NotifyToObservers(DamageableUnitNotyfications.UpdateHealthPoints);

         if(actualHealthPoints == 0)
         {
            Die = true;
            NotifyToObservers(DamageableUnitNotyfications.Death);
            OnDie.Invoke();
         }
      }

      public void RecoverHealthPoints(float argAmountOfHealthPoints)
      {
         if(Die)
            return;

         actualHealthPoints += argAmountOfHealthPoints;
         actualHealthPoints = Mathf.Clamp(actualHealthPoints, 0, maxHealthPoints);
         NotifyToObservers(DamageableUnitNotyfications.UpdateHealthPoints);
      }

      public void RecoverShieldPoints(float argAmountOfShieldPoints)
      {
         if(Die || !hasShieldPoints)
            return;

         actualShieldPoints += argAmountOfShieldPoints;
         actualShieldPoints = Mathf.Clamp(actualShieldPoints, 0, maxShieldPoints);
         NotifyToObservers(DamageableUnitNotyfications.UpdateShieldPoints);
      }

      public bool HaveThisQuantityOfHealthPoints(float argAmountHealthPoints)
      {
         return (actualHealthPoints - argAmountHealthPoints) >= 0;
      }

      public bool HaveThisQuantityOfShieldPoints(float argAmountShieldPoints)
      {
         return (actualHealthPoints - argAmountShieldPoints) >= 0;
      }
   }

   public enum DamageableUnitNotyfications
   {
      Death,
      UpdateHealthPoints,
      UpdateShieldPoints,
      ResetHealthPointsToMaxValue
   }
}