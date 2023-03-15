using System;
using System.Collections.Generic;
using UnityEngine;

namespace Store
{
   [Serializable]
   public class ListTradeableObjectsJSON
   {
      [SerializeField]
      private List<string> listTradeableObjects = new List<string>();

      public List<string> ListTradeableObjects
         => listTradeableObjects;

      public void AddTradeableObject(AbstractSOTradeableObject argTradeableObject)
      {
         listTradeableObjects.Add(argTradeableObject.TradeableObjectId);
      }
   }
}