namespace BoxMessage
{
   public class BoxMessageInfo : BoxMessage
   {
      protected override void OnButtonAccept()
      {
         base.OnButtonAccept();
         ClosePanelAndDestroy();
      }
   }
}