namespace Store
{
   public class StoreUIBasic : InventoryUI
   {
      private void OnEnable()
      {
         CreateAllLotsOnContainerLots();
      }
   }
}