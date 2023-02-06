using UnityEngine;

namespace Store
{
   /// <summary>
   /// inventario base de un usuario
   /// </summary>
   public class BaseInventory : AbstractStore
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
         refLoadStore.CargarFromPlayersPrefs();
      }
      
   }
}