using Patterns.Observer;
using PoolSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Damageable
{
   public class DamageableObject : AbstractSubject
   {
      [Header("Health")]
      [SerializeField]
      private float maxHealth;

      private float actualHealth;

      [Header("Shield")]
      [SerializeField]
      private bool hasShield;

      [SerializeField]
      private float maxShield;

      private float actualShield;

      [SerializeField]
      private bool canRegenerateShield;

      [SerializeField]
      private float quantityShieldRegeneratedPerSecond = 10f;

      [SerializeField]
      private float timeForRegenerateShield = 2f;

      private float actualTimeWithoutRegenerateShield;

      [Header("Health bar references")]
      [SerializeField]
      private HealthBarUI refHealthBarUI;

      [SerializeField]
      private bool createHealthBar;

      [SerializeField]
      private GameObject prefabHealthBar;

      [SerializeField]
      private Vector3 healthBarPositionOffset;

      private Camera refCamera;

      private Canvas refCanvasDynamicObjects;

      public UnityEvent OnDie;

      [SerializeField]
      private bool canPositionHealthBarOnCanvas = true;

      public bool Die { get; private set; }

      private void Awake()
      {
         refCamera = Camera.main;

         var tmpCanvasDynamic = GameObject.FindWithTag("CanvasDynamic");

         if(tmpCanvasDynamic)
         {
            refCanvasDynamicObjects = tmpCanvasDynamic.GetComponent<Canvas>();
         }
         else
            Debug.LogError("Canvas with tag CanvasDynamic does exist");
      }

      private void OnEnable()
      {
         ResetHealthToMaxHealth();
      }

      private void Update()
      {
         RegenerateShield();
      }

      private void RegenerateShield()
      {
         if(Die)
            return;

         if(hasShield)
         {
            if(canRegenerateShield)
            {
               actualTimeWithoutRegenerateShield -= Time.deltaTime;
               actualTimeWithoutRegenerateShield = Mathf.Clamp(actualTimeWithoutRegenerateShield, 0f, float.MaxValue);

               if(actualTimeWithoutRegenerateShield == 0)
               {
                  actualShield += quantityShieldRegeneratedPerSecond * Time.deltaTime;
                  actualShield = Mathf.Clamp(actualShield, 0f, maxShield);
                  refHealthBarUI.UpdateUIShield(actualShield / maxShield);
               }
            }
         }
      }

      private void LateUpdate()
      {
         PositionHealthBarOnScreen();
      }

      private void PositionHealthBarOnScreen()
      {
         if(canPositionHealthBarOnCanvas)
            refHealthBarUI.SetPositionOnCanvas(refCamera.WorldToScreenPoint(transform.position + healthBarPositionOffset));
      }

      private void ResetHealthToMaxHealth()
      {
         Die = false;

         if(createHealthBar)
            refHealthBarUI = Pool.Instance.InstantiateGameObjectPooleable<HealthBarUI>(prefabHealthBar, transform.position, Quaternion.identity, refCanvasDynamicObjects.transform);

         refHealthBarUI.OwnerIsAlive = true;
         actualHealth = maxHealth;
         actualTimeWithoutRegenerateShield = timeForRegenerateShield;

         if(hasShield)
         {
            actualShield = maxShield;
            refHealthBarUI.UpdateUIHealthAndShield(actualShield / maxShield, actualHealth / maxHealth);
         }
         else
            refHealthBarUI.UpdateUIHealth(actualHealth / maxHealth);
      }

      public void RemoveHealth(float argHealthToRemove)
      {
         if(Die)
            return;

         if(hasShield)
         {
            actualTimeWithoutRegenerateShield = timeForRegenerateShield;

            if(actualShield > 0)
            {
               actualShield -= argHealthToRemove;

               if(actualShield < 0)
               {
                  actualHealth -= Mathf.Abs(actualShield);
                  actualShield = 0;
               }

               refHealthBarUI.UpdateUIHealthAndShield(actualShield / maxShield, actualHealth / maxHealth);
            }
            else
            {
               actualHealth -= argHealthToRemove;
               refHealthBarUI.UpdateUIHealth(actualHealth / maxHealth);
            }
         }
         else
         {
            actualHealth -= argHealthToRemove;
            refHealthBarUI.UpdateUIHealth(actualHealth / maxHealth);
         }

         actualHealth = Mathf.Clamp(actualHealth, 0, int.MaxValue);
         NotifyToObservers(actualHealth <= 0? DamageableUnitNotyfications.Death : DamageableUnitNotyfications.RemoveHealth);

         if(actualHealth <= 0)
         {
            Die = true;
            refHealthBarUI.OwnerIsAlive = false;
            OnDie.Invoke();
         }
      }

      public void RecoverHealth(float argAmountOfHealing)
      {
         if(Die)
            return;

         actualHealth += argAmountOfHealing;
         actualHealth = Mathf.Clamp(actualHealth, 0, maxHealth);
         refHealthBarUI.UpdateUIHealth(actualHealth / maxHealth);
      }

      public void RecoverShield(float argAmountOfShield)
      {
         if(Die || !hasShield)
            return;

         actualShield += argAmountOfShield;
         actualShield = Mathf.Clamp(actualShield, 0, maxShield);
         refHealthBarUI.UpdateUIHealthAndShield(actualShield / maxShield, actualHealth / maxHealth);
      }

      public enum DamageableUnitNotyfications
      {
         Death,
         RemoveHealth
      }
   }
}