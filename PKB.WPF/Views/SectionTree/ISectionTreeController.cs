using PKB.WPF.Common;
using PKB.WPF.Common.Interfaces;

namespace PKB.WPF.Views.SectionTree
{
    public interface ISectionTreeController : IScreen
    {
        void SetResource(ResourceViewModel resource);
    }
}
