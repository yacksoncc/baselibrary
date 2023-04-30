namespace Store
{
   public class InventoryBasic : Inventory
   {
      private ISaveInventory refSaveInventory;

      private ILoadInventory refLoadInventory;

      private void Awake()
      {
         var tmpSaveLoadInventory = new SaveLoadInventoryPlayerPrefs();
         refSaveInventory = tmpSaveLoadInventory;
         refLoadInventory = tmpSaveLoadInventory;
      }

      private void OnDisable()
      {
         refSaveInventory.Save(this);
      }

      private void OnEnable()
      {
         refLoadInventory.Load(this);
      }
   }
}