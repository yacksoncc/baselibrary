namespace BoxMessage
{
   public class BoxMessageDecision : BoxMessage
   {
      protected override void OnButtonAccept()
      {
         base.OnButtonAccept();
         ClosePanelAndDestroy();
      }

      protected override void OnButtonCancel()
      {
         base.OnButtonCancel();
         ClosePanelAndDestroy();
      }
   }
}