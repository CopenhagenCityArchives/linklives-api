using linklives_api_dal.domain;
using System.Collections.Generic;

namespace linklives_api_dal.Repositories
{
    public interface ILinkRepository
    {
        int Count();
        IEnumerable<Link> GetAll();
        Link GetByKey(string linkKey);
        void Insert(Link link);
        void Insert(IEnumerable<Link> links);
        void Upsert(IEnumerable<Link> entitties);
        void Delete(string linkKey);
        void Save();
    }
}
