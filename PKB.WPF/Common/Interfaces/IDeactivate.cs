using System;

namespace PKB.WPF.Common.Interfaces
{
    public interface IDeactivate
    {
        event EventHandler Deactivated;

        void Deactivate();
    }
}
