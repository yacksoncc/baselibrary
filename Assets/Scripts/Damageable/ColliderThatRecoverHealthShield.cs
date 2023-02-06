using UnityEngine;
using UnityEngine.Events;

namespace Damageable
{
   public class ColliderThatRecoverHealthShield : MonoBehaviour
   {
      [SerializeField]
      private RecoveryType recoveryType;

      [SerializeField]
      private ColliderShape colliderShape;

      [SerializeField]
      private float radiusCollision;

      [SerializeField]
      private Bounds boundsCollision;

      [SerializeField]
      private LayerMask layerMaskForCollision;

      [SerializeField]
      private bool recoverOneTimeOnly;

      [SerializeField]
      private float minAmount;

      [SerializeField]
      private float maxAmount;

      [SerializeField]
      private bool recoverPerSecond;

      [SerializeField]
      private float minAmountPerSecond;

      [SerializeField]
      private float maxAmountPerSecond;

      [SerializeField]
      private UnityEvent OnApplyRecovery;

      public bool CanApplyRecovery { get; set; }

      private void OnEnable()
      {
         CanApplyRecovery = true;
      }

      private void Update()
      {
         CheckCollision();
      }

      private void OnDrawGizmos()
      {
         Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
         Gizmos.color = Color.red;

         switch(colliderShape)
         {
            case ColliderShape.Sphere:
               Gizmos.DrawWireSphere(Vector3.zero, radiusCollision);

               break;

            case ColliderShape.Box:
               Gizmos.DrawWireCube(Vector3.zero, boundsCollision.extents * 2);
               break;
         }
      }

      private void CheckCollision()
      {
         if(!CanApplyRecovery)
            return;

         Collider[] tmpColliders = new Collider[0];

         switch(colliderShape)
         {
            case ColliderShape.Sphere:
               tmpColliders = Physics.OverlapSphere(transform.position, radiusCollision, layerMaskForCollision);

               break;

            case ColliderShape.Box:
               tmpColliders = Physics.OverlapBox(transform.position + boundsCollision.center, boundsCollision.extents, transform.rotation, layerMaskForCollision);

               break;
         }

         if(tmpColliders.Length == 0)
            return;

         if(recoverPerSecond)
         {
            foreach(var tmpCollider in tmpColliders)
            {
               var tmpDamageableUnit = tmpCollider.GetComponent<DamageableObject>();

               if(!tmpDamageableUnit)
                  continue;

               if(recoveryType == RecoveryType.Healing)
                  tmpDamageableUnit.RecoverHealth(Random.Range(minAmountPerSecond, maxAmountPerSecond) * Time.deltaTime);
               else if(recoveryType == RecoveryType.Shield)
                  tmpDamageableUnit.RecoverShield(Random.Range(minAmountPerSecond, maxAmountPerSecond) * Time.deltaTime);
            }
         }
         else if(recoverOneTimeOnly)
         {
            foreach(var tmpCollider in tmpColliders)
            {
               var tmpDamageableUnit = tmpCollider.GetComponent<DamageableObject>();

               if(!tmpDamageableUnit)
                  continue;

               if(recoveryType == RecoveryType.Healing)
                  tmpDamageableUnit.RecoverHealth(Random.Range(minAmount, maxAmount));
               else if(recoveryType == RecoveryType.Shield)
                  tmpDamageableUnit.RecoverShield(Random.Range(minAmount, maxAmount));
            }

            CanApplyRecovery = false;
         }

         OnApplyRecovery.Invoke();
      }

      enum ColliderShape
      {
         Sphere,
         Box
      }

      enum RecoveryType
      {
         Healing,
         Shield
      }
   }
}