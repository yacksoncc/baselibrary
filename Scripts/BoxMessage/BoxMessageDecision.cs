
namespace BoxMessage
{
    public class BoxMessageDecision : AbstractBoxMessage
    {
        protected override void OnButtonAccept()
        {
            base.OnButtonAccept();
            ShowPanel(false, true);
        }

        protected override void OnButtonCancel()
        {
            base.OnButtonCancel();
            ShowPanel(false, true);
        }
    }
}
