using System;

namespace PKB.WPF.Interactivity
{
    public class Notification : InteractionBase
    {
        public event Action Ended;

        protected override void OnEnd()
        {
            if (Ended != null)
                Ended.Invoke();
        }
    }
}
