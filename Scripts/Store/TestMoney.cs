using UnityEngine;

namespace Store
{
   public class TestMoney : MonoBehaviour
   {
      [SerializeField]
      private SOCoin refCoinForTest;

      [ContextMenu("Add1000ToMoneda")]
      public void Add1000ToMoneda()
      {
         refCoinForTest.AddThisQuantity(1000);
      }

      [ContextMenu("Add10000ToMoneda")]
      public void Add10000ToMoneda()
      {
         refCoinForTest.AddThisQuantity(10000);
      }

      [ContextMenu("Add100000ToMoneda")]
      public void Add100000ToMoneda()
      {
         refCoinForTest.AddThisQuantity(100000);
      }
   }
}