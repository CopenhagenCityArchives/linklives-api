using linklives_api_dal.domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public interface IPersonAppearanceRepository
    {
        JObject GetById(string Id);
        List<dynamic> GetByIds(List<string> Ids);
    }
}
