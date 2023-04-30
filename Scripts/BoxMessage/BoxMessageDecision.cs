namespace BoxMessage
{
   public class BoxMessageDecision : BoxMessage
   {
      protected override void OnButtonAccept()
      {
         base.OnButtonAccept();
         ClosePanelAndDestroyIt();
      }

      protected override void OnButtonCancel()
      {
         base.OnButtonCancel();
         ClosePanelAndDestroyIt();
      }
   }
}