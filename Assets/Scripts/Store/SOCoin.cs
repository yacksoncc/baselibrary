using UnityEditor;
using UnityEngine;

namespace Store
{
   /// <summary>
   /// Coin to trade objects between stores
   /// </summary>
   [CreateAssetMenu(fileName = "soCoin", menuName = "Store/soCoin", order = 0)]
   public class SOCoin : ScriptableObject
   {
      [SerializeField]
      private Sprite icon;

      [SerializeField]
      private string coinName;

      [SerializeField, HideInInspector]
      private string coinId;

      public string CoinName
         => coinName;

      public string CoinId
      {
         get
         {
            GenerateNewID();
            return coinId;
         }
      }

      public int Quantity
      {
         get
         {
            return PlayerPrefs.GetInt(coinId, 0);
         }
         set
         {
            PlayerPrefs.SetInt(coinId, value);
         }
      }

      public bool ThereIsThisQuantity(int argQuantity)
      {
         return (Quantity - argQuantity) >= 0;
      }

      public void SpentThisQuantity(int argQuantity)
      {
         Quantity -= argQuantity;
      }

      public bool ConsumeThisQuantityOnlyIfThereIsEnough(int argQuantity)
      {
         if(!ThereIsThisQuantity(argQuantity))
            return false;

         SpentThisQuantity(argQuantity);
         return true;
      }

      public void AddThisQuantity(int argQuantity)
      {
         Quantity += argQuantity;
      }

      private void GenerateNewID()
      {
         GUIDGeneretor.GenerateNewID(ref coinId);
#if UNITY_EDITOR
         EditorUtility.SetDirty(this);
#endif
      }
   }
}