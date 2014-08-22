using System;

namespace PKB.WPF.Common.Interfaces
{
    public interface IActivate
    {
        event EventHandler Activated;

        void Activate();

        bool IsActive { get; }
    }
}
