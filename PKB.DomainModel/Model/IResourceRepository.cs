using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.DomainModel.Common;

namespace PKB.DomainModel.Model
{
    public interface IResourceRepository
    {
        Resource Get(ResourceId id);
    }
}
