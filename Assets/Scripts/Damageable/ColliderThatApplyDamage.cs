using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Damageable
{
   public class ColliderThatApplyDamage : MonoBehaviour
   {
      [SerializeField]
      private ColliderShape colliderShape;

      [SerializeField]
      private float radiusCollision;

      [SerializeField]
      private Bounds boundsCollision;

      [SerializeField]
      private LayerMask layerMaskForCollision;

      [SerializeField]
      private bool hitOneTimeOnly;

      [SerializeField]
      private float minDamagePerHit;

      [SerializeField]
      private float maxDamageHit;

      [SerializeField]
      private bool hitPerSecond;

      [SerializeField]
      private float minDamagePerSecond;

      [SerializeField]
      private float maxDamagePerSecond;

      [SerializeField]
      private bool applyImpulse;

      [SerializeField]
      private float impulseForce = 5f;

      [SerializeField]
      private UnityEvent OnApplyDamage;

      public bool CanApplyDamage { get; set; }

      private void OnEnable()
      {
         CanApplyDamage = true;
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
         if(!CanApplyDamage)
            return;

         Collider[] tmpColliders = Array.Empty<Collider>();

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

         if(hitPerSecond)
         {
            foreach(var tmpCollider in tmpColliders)
            {
               var tmpDamageableUnit = tmpCollider.GetComponent<DamageableObject>();

               if(tmpDamageableUnit)
                  tmpDamageableUnit.RemoveHealthPoints(Random.Range(minDamagePerSecond, maxDamagePerSecond) * Time.deltaTime);

               if(applyImpulse)
               {
                  var tmpApplyImpulse = tmpCollider.GetComponent<ApplyImpulseRigibody>();

                  if(tmpApplyImpulse)
                     tmpApplyImpulse.ExecuteImpulse((transform.forward + Vector3.up).normalized, impulseForce);
               }
            }
         }
         else if(hitOneTimeOnly)
         {
            foreach(var tmpCollider in tmpColliders)
            {
               var tmpDamageableUnit = tmpCollider.GetComponent<DamageableObject>();

               if(tmpDamageableUnit)
                  tmpDamageableUnit.RemoveHealthPoints(Random.Range(minDamagePerHit, maxDamageHit));

               if(applyImpulse)
               {
                  var tmpApplyImpulse = tmpCollider.GetComponent<ApplyImpulseRigibody>();

                  if(tmpApplyImpulse)
                     tmpApplyImpulse.ExecuteImpulse(transform.forward + Vector3.up, impulseForce);
               }
            }

            CanApplyDamage = false;
         }

         OnApplyDamage.Invoke();
      }

      enum ColliderShape
      {
         Sphere,
         Box
      }
   }
}