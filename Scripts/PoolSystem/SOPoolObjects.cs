using UnityEngine;

namespace PoolSystem
{
   [CreateAssetMenu(menuName = "Pool/SOPoolObjects", fileName = "SOPoolObjects", order = 0)]
   public class SOPoolObjects : ScriptableObject
   {
      public ObjectPoolWrapper[] arrayObjectPoolWrapper;
   }
}