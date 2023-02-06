using UnityEngine;

namespace Damageable
{
   [RequireComponent(typeof(Rigidbody))]
   public class ApplyImpulseRigibody : MonoBehaviour, IApplyImpulse
   {
      private Rigidbody refRigidbody;

      [SerializeField]
      private float factorForce;

      public float FactorForce
      {
         get => factorForce;
         set => factorForce = value;
      }

      private void Awake()
      {
         refRigidbody = GetComponent<Rigidbody>();
      }

      public void ExecuteImpulse(Vector3 argImpulseNormalized, float argFactorForce)
      {
         refRigidbody.AddForce(argImpulseNormalized.normalized * argFactorForce * factorForce, ForceMode.VelocityChange);
      }
   }

   public interface IApplyImpulse
   {
      public void ExecuteImpulse(Vector3 argImpulseNormalized, float argFactorForce);
   }
}