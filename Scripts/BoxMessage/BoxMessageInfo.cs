#pragma warning disable 0649
namespace BoxMessage
{
   public class BoxMessageInfo : AbstractBoxMessage
   {
      protected override void OnButtonAccept()
      {
         base.OnButtonAccept();
         ClosePanelAndDestroyIt();
      }
   }
}