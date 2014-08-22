using System;

namespace PKB.WPF.Common.Interfaces
{
    public interface IClose
    {
        event EventHandler Closed;

        void Close();
    }
}
