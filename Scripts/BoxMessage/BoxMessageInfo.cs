#pragma warning disable 0649
using UnityEngine;

namespace BoxMessage
{
    public class BoxMessageInfo : AbstractBoxMessage
    {
        protected override void OnButtonAccept()
        {
            base.OnButtonAccept();
            ShowPanel(false, true);
        }
    }
}