#pragma warning disable
using UnityEngine;

namespace Store
{
   /// <summary>
   /// Moneda para poder tradear objetos entre almacenes de objetos
   /// </summary>
   [CreateAssetMenu(fileName = "Moneda", menuName = "Tienda/Moneda", order = 0)]
   public class SOCoin : ScriptableObject
   {
      [SerializeField]
      private Sprite icono;

      [SerializeField]
      private string nombre;

      public string Nombre
         => nombre;

      public int Cantidad
      {
         get
         {
            return PlayerPrefs.GetInt("Moneda" + nombre, 0);
         }
         set
         {
            PlayerPrefs.SetInt("Moneda" + nombre, value);
         }
      }

      public bool HayEstaCantidad(int argCantidad)
      {
         return (Cantidad - argCantidad) >= 0;
      }

      public void GastarEstaCantidad(int argCantidad)
      {
         Cantidad -= argCantidad;
         Debug.Log("Cantidad nueva de dinero : " + Cantidad + " cantidad gastada " + argCantidad);
      }

      public bool ConsumeThisQuantityOnlyIfThereIsEnough(int argCantidad)
      {
         if(!HayEstaCantidad(argCantidad))
            return false;

         GastarEstaCantidad(argCantidad);
         return true;
      }

      public void AgregarEstaCantidad(int argCantidad)
      {
         Cantidad += argCantidad;
      }
   }
}