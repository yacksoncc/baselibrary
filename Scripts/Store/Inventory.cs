using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace Store
{
   /// <summary>
   /// Derive for create a new store that can be a store in game or user inventory or any type of trade in game
   /// </summary>
   public abstract class Inventory : MonoBehaviour
   {
      [SerializeField]
      private string id;

      [SerializeField]
      protected List<SOLot> listSoLots;

      public string ID
      {
         get
         {
            GenerateNewID();
            return id;
         }
      }

      private void OnValidate()
      {
         GenerateNewID();
      }

      public Collection<SOLot> GetAllLotsAvaiblesInInventory()
      {
         var tmpCollectionLotsAvaibles = new Collection<SOLot>();

         foreach(var tmpSoLot in listSoLots)
            if(tmpSoLot.HaveQuantityAvaible)
               tmpCollectionLotsAvaibles.Add(tmpSoLot);

         return tmpCollectionLotsAvaibles;
      }

      public void AddNewTradeableObject(SOTradeableObject argSoTradeableObject, int argQuantity)
      {
         var tmpNewLot = ScriptableObject.CreateInstance<SOLot>();
         tmpNewLot.SetNewSOTradeableObject(argSoTradeableObject);
         tmpNewLot.AddQuantity(argQuantity);
         listSoLots.Add(tmpNewLot);
      }

      public int GetTotalPriceOfAllLots()
      {
         int tmpPriceOfAllLots = 0;

         foreach(var tmpSoLoteObjeto in listSoLots)
            tmpPriceOfAllLots += tmpSoLoteObjeto.GetWholePriceOfThisLot();

         return tmpPriceOfAllLots;
      }

      public virtual bool TradeLotFromThisInventoryToOtherInventory(OrderTrading argOrderTrading, Inventory argFinalInventoryOwnerOfLot)
      {
         var tmpSoLot = listSoLots.Find(argLoteObjeto => argLoteObjeto.RefSoTradeableObject == argOrderTrading.refSoTradeableObject);

         if(tmpSoLot)
         {
            var tmpSoTradeableObject = argOrderTrading.refSoTradeableObject;
            var tmpQuantity = argOrderTrading.quantity;

            if(tmpSoLot.ThereIsQuantity(tmpQuantity))
            {
               if(tmpSoTradeableObject.SoCoinForBuyThisTradeableObject.ConsumeThisQuantityOnlyIfThereIsEnough(tmpSoTradeableObject.Price * tmpQuantity))
               {
                  tmpSoLot.RemoveQuantity(tmpQuantity);
                  argFinalInventoryOwnerOfLot.AddTradingOrderToThisInventory(argOrderTrading);
                  return true;
               }

               Debug.Log("cantidad de dinero insuficiente", this);
            }
            else
               Debug.Log("cantidad insuficiente del objeto", this);
         }

         return false;
      }

      public void AddTradingOrderToThisInventory(OrderTrading argOrderTrading)
      {
         var tmpSoLot = listSoLots.Find(argLoteObjeto => argLoteObjeto.RefSoTradeableObject == argOrderTrading.refSoTradeableObject);

         if(tmpSoLot)
         {
            var tmpQuantity = argOrderTrading.quantity;
            tmpSoLot.AddQuantity(tmpQuantity);
         }
         else
            AddNewTradeableObject(argOrderTrading.refSoTradeableObject, argOrderTrading.quantity);
      }

      public void AddQuantityOfTradeableObjectById(string argSoTradeableObjectId, int argQuantity)
      {
         var tmpSoLot = listSoLots.Find(argLoteObjeto => argLoteObjeto.RefSoTradeableObject.ID == argSoTradeableObjectId);

         if(tmpSoLot)
            tmpSoLot.AddQuantity(argQuantity);
         else
            Debug.LogWarning($"The argSoTradeableObjectId : {argSoTradeableObjectId} ; no exists in this inventory : {name}.", this);
      }

      public void SetQuantityOfTradeableObjectById(string argSoTradeableObjectId, int argQuantity)
      {
         var tmpSoLot = listSoLots.Find(argLoteObjeto => argLoteObjeto.RefSoTradeableObject.ID == argSoTradeableObjectId);

         if(tmpSoLot)
            tmpSoLot.SetQuantity(argQuantity);
         else
            Debug.LogWarning($"The argSoTradeableObjectId : {argSoTradeableObjectId} ; no exists in this inventory : {name}.", this);
      }

      public bool ThereIsThisSOLot(SOLot argSoLot)
      {
         return listSoLots.Contains(argSoLot);
      }

      private void GenerateNewID()
      {
         GUIDGeneretor.GenerateNewID(ref id);

#if UNITY_EDITOR
         EditorUtility.SetDirty(this);
#endif
      }
   }
}