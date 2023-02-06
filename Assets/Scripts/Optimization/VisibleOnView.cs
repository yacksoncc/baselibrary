using Extensions;
using UnityEngine;

namespace Optimization
{
   public class VisibleOnView : MonoBehaviour
   {
      private bool visible;

      public UnityEventBool OnVisible;

      public bool Visible
         => visible;

      private void OnBecameVisible()
      {
         visible = true;
         ExecuteEventOnVisible();
      }

      private void OnBecameInvisible()
      {
         visible = false;
         ExecuteEventOnVisible();
      }

      private void ExecuteEventOnVisible()
      {
         OnVisible.Invoke(visible);
      }
   }
}