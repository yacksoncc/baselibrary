using UnityEngine;

namespace Store
{
   public class InventoryBasic : AbstractStore
   {
      [SerializeField]
      private SaveStore refSaveStore;

      [SerializeField]
      private LoadStore refLoadStore;

      private void OnDisable()
      {
         refSaveStore.Save(this);
      }

      private void OnEnable()
      {
         refLoadStore.RefAbstractStore = this;
         refLoadStore.LoadFromPlayersPrefs();
      }
   }
}