using System;

namespace PKB.WPF.Interactivity
{
    public abstract class InteractionBase : IInteraction
    {
        private bool _isEnded;

        public event EventHandler Ending;

        public void End()
        {
            if (_isEnded)
                throw new InvalidOperationException();

            _isEnded = true;

            OnEding();
            OnEnd();
        }

        protected abstract void OnEnd();

        private void OnEding()
        {
            if (Ending != null)
                Ending.Invoke(this, EventArgs.Empty);
        }
    }
}
