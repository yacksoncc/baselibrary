#pragma warning disable
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Store
{
   /// <summary>
   /// Derive for create a new store that can be a store in game or user inventory or any type of trade in game
   /// </summary>
   public abstract class AbstractStore : MonoBehaviour
   {
      [SerializeField]
      private string storeID;

      [SerializeField]
      protected List<SOLot> listSoLots;

      public string StoreID
         => storeID;

      private void OnEnable()
      {
         GenerateNewID();
      }

      public List<SOLot> GetAllLotsAvaiblesOnStore()
      {
         var tmpListLotsAvaiblesOnStore = new List<SOLot>();

         foreach(var tmpSoLot in listSoLots)
            if(tmpSoLot.LotHasQuantityAvaible)
               tmpListLotsAvaiblesOnStore.Add(tmpSoLot);

         return tmpListLotsAvaiblesOnStore;
      }

      public void AddNewTradeableObject(AbstractSOTradeableObject argSoTradeableObject, int argCantidad)
      {
         var tmpNewLot = ScriptableObject.CreateInstance<SOLot>();
         tmpNewLot.SetNewSOTradeableObject(argSoTradeableObject);
         tmpNewLot.AddQuantity(argCantidad);
         listSoLots.Add(tmpNewLot);
      }

      public int GetTotalPriceOfAllLots()
      {
         int tmpPriceOfAllLots = 0;

         foreach(var tmpSoLoteObjeto in listSoLots)
            tmpPriceOfAllLots += tmpSoLoteObjeto.GetWholePriceOfThisLot();

         return tmpPriceOfAllLots;
      }

      public virtual bool TradeLotFromThisStoreToOtherStore(OrderTrading argOrderTrading, AbstractStore argFinalStoreOwnerOfLot)
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
                  argFinalStoreOwnerOfLot.AddTradingOrder(argOrderTrading);
                  return true;
               }

               Debug.Log("cantidad de dinero insuficiente");
            }
            else
               Debug.Log("cantidad insuficiente del objeto");
         }

         return false;
      }

      public void AddTradingOrder(OrderTrading argOrderTrading)
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

      public bool ThereIsThisSOLot(SOLot argSoLot)
      {
         return listSoLots.Contains(argSoLot);
      }

      [ContextMenu("GenerateNewID")]
      private void GenerateNewID()
      {
#if UNITY_EDITOR
         if(storeID == "")
         {
            var tmpNewID = Guid.NewGuid().ToString();
            var tmpArraySplit = tmpNewID.Split('-');
            storeID = tmpArraySplit[0];
            EditorUtility.SetDirty(this);
         }
#endif
      }
   }
}