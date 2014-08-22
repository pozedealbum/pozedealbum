using PKB.WPF.Common.Interfaces;
using System;

namespace PKB.WPF.Common
{
    public class Screen<TViewModel> : Presenter<TViewModel>, IScreen
         where TViewModel : class, new()
    {
        private bool _isClosed;
        private bool _hasBeenActivatedEver;

        public event EventHandler Activated;

        public event EventHandler Closed;

        public event EventHandler Deactivated;

        public bool IsActive { get; private set; }

        protected virtual void OnInitialActivate()
        {
        }

        protected virtual void OnActivate()
        {
        }

        protected virtual void OnDeactivate()
        {
        }

        protected virtual void OnClose()
        {
        }

        public void Activate()
        {
            if (IsActive)
                return;

            IsActive = true;
            _isClosed = false;

            if (!_hasBeenActivatedEver)
                OnInitialActivate();

            _hasBeenActivatedEver = true;

            OnActivate();

            var handler = Activated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void Close()
        {
            if (_isClosed)
                return;

            (this as IDeactivate).Deactivate();

            _isClosed = true;

            OnClose();

            var handler = Closed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            _isClosed = false;

            OnDeactivate();

            var handler = Deactivated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}