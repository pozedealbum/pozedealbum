using System;

namespace PKB.WPF.Interactivity
{
    public class Confirmation : InteractionBase
    {
        public event Action Confirmed;
        public event Action Canceled;

        private bool _isConfirmed;

        public void Confirm()
        {
            _isConfirmed = true;
            End();
        }

        public void Cancel()
        {
            _isConfirmed = false;
            End();
        }

        protected override void OnEnd()
        {
            if (_isConfirmed)
                OnCofirmed();
            else
                OnCanceled();
        }


        private void OnCofirmed()
        {
            if (Confirmed != null)
                Confirmed.Invoke();
        }

        private void OnCanceled()
        {
            if (Canceled != null)
                Canceled.Invoke();
        }
    }

}
