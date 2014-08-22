using PKB.DomainModel;
using PKB.WPF.Common.Interfaces;

namespace PKB.WPF.Views.SectionTree
{
    public interface ISectionTreeController : IViewAware
    {
        void ChangeRoot(Section root);
    }
}
