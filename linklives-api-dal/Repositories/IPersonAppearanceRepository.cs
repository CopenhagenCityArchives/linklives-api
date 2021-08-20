using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace linklives_api_dal.Repositories
{
    public interface IPersonAppearanceRepository
    {
        JObject GetById(string Id);
        List<dynamic> GetByIds(List<string> Ids);
    }
}
