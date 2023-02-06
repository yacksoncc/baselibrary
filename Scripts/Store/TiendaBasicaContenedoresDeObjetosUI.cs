#pragma warning disable

namespace Store
{
   /// <summary>
   /// Muestra todos los objetos de la tienda en pantalla
   /// </summary>
   public class TiendaBasicaContenedoresDeObjetosUI : AbstractStoreUI
   {
      private void OnEnable()
      {
         CreateAllLotsOnContainerLots();
      }
   }
}