using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
   public abstract class LotUI : MonoBehaviour
   {
      [SerializeField]
      private Image imageIcon;

      [SerializeField]
      private TMP_Text textQuantity;

      protected SOLot soLot;

      protected Inventory RefInventoryOwner;

      public void SetLot(SOLot argSoLot, Inventory argRefInventory)
      {
         RefInventoryOwner = argRefInventory;
         soLot = argSoLot;
         imageIcon.sprite = soLot.SpriteObjetoTienda;
         textQuantity.text = soLot.ActualQuantity.ToString();
      }

      public abstract void ButtonExecuteTrade();
   }
}