using System.Collections.Generic;

namespace PKB.DomainModel
{
    public interface ISectionRepository
    {
        IEnumerable<Section> List();
    }
}
