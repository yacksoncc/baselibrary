using UnityEditor;
using UnityEngine;

namespace Store
{
   public abstract class SOTradeableObject : ScriptableObject
   {
      [SerializeField, HideInInspector]
      private string id;

      [SerializeField]
      private string objectName;

      [SerializeField]
      private Sprite sprite;

      [SerializeField]
      private int price;

      [SerializeField]
      private SOCoin soCoinForBuyThisTradeableObject;

      public string ObjectName
         => objectName;

      public Sprite Sprite
         => sprite;

      public int Price
         => price;

      public SOCoin SoCoinForBuyThisTradeableObject
         => soCoinForBuyThisTradeableObject;

      public string ID
         => id;

      private void Awake()
      {
         GenerateNewID();
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