using System;
using Patterns.Observer;
using PoolSystem;
using UnityEngine;

namespace Damageable
{
   [RequireComponent(typeof(DamageableObject))]
   public class HealthAndShieldPointsBarHandle : MonoBehaviour, IObserver
   {
      private DamageableObject refDamageableObject;

      [Header("Health bar references")]
      [SerializeField]
      private HealthPointsBarUI refHealthPointsBarUI;

      [SerializeField]
      private bool createHealthPointsBar;

      [SerializeField]
      private GameObject prefabHealthPointsBar;

      [SerializeField]
      private Vector3 healthPointsBarPositionOffset;

      private Camera refCamera;

      private Canvas refCanvasDynamicObjects;

      [SerializeField]
      private bool canUpdatePositionHealthPointsBarOnCanvas = true;

      private void Awake()
      {
         refDamageableObject = GetComponent<DamageableObject>();
         refDamageableObject.SuscribeObserver(this);

         refCamera = Camera.main;

         var tmpCanvasDynamic = GameObject.FindWithTag("CanvasDynamic");

         if(tmpCanvasDynamic)
            refCanvasDynamicObjects = tmpCanvasDynamic.GetComponent<Canvas>();
         else
            Debug.LogError("Canvas with tag \"CanvasDynamic\" does exist");
      }

      private void LateUpdate()
      {
         PositionHealthPointsBarOnScreen();
      }

      private void PositionHealthPointsBarOnScreen()
      {
         if(canUpdatePositionHealthPointsBarOnCanvas)
            refHealthPointsBarUI.SetPositionOnCanvas(refCamera.WorldToScreenPoint(transform.position + healthPointsBarPositionOffset));
      }

      public void UpdateFromSubject(Enum argFlag)
      {
         switch(argFlag)
         {
            case DamageableUnitNotyfications.Death:
               refHealthPointsBarUI.OwnerIsAlive = false;
               break;

            case DamageableUnitNotyfications.UpdateHealthPoints:
               UpdateHealthPoints();
               break;

            case DamageableUnitNotyfications.UpdateShieldPoints:
               UpdateShieldPoints();
               break;

            case DamageableUnitNotyfications.ResetHealthPointsToMaxValue:
               ResetHealthPointsToMaxValues();
               break;
         }
      }

      private void ResetHealthPointsToMaxValues()
      {
         if(createHealthPointsBar)
            refHealthPointsBarUI = Pool.Instance.InstantiateGameObjectPooleable<HealthPointsBarUI>(prefabHealthPointsBar, transform.position, Quaternion.identity, refCanvasDynamicObjects.transform);

         refHealthPointsBarUI.OwnerIsAlive = true;
      }

      private void UpdateHealthPoints()
      {
         refHealthPointsBarUI.UpdateUIHealthPoints(refDamageableObject.ActualHealthPointsNormalized);
      }

      private void UpdateShieldPoints()
      {
         refHealthPointsBarUI.UpdateUIShieldPoints(refDamageableObject.ActualShieldPointsNormalized);
      }
   }
}