using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace linklives_api_dal.Repositories
{
    public interface IPersonAppearanceRepository
    {
        dynamic GetById(string Id);
        IEnumerable<dynamic> GetByIds(List<string> Ids);
    }
}
