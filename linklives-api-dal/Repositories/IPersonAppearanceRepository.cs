using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public interface IPersonAppearanceRepository
    {
        PersonAppearance GetById(string Id);
        string GetRawJsonById(string Id);
    }
}
