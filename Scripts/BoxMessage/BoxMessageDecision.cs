namespace BoxMessage
{
   public class BoxMessageDecision : AbstractBoxMessage
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