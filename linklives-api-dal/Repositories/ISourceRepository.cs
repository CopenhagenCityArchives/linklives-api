using linklives_api_dal.domain;
using System.Collections.Generic;

namespace linklives_api_dal.Repositories
{
    public interface ISourceRepository
    {
        public IEnumerable<dynamic> GetAll();
        public dynamic GetById(int id);
        public IEnumerable<dynamic> GetByIds(IList<int> ids);
    }
}
