using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFLinkRepository : ILinkRepository
    {
        private readonly LinklivesContext context;

        public EFLinkRepository(LinklivesContext context)
        {
            this.context = context;
        }

        public void DeleteLink(int linkKey)
        {
            throw new NotImplementedException();
        }

        public Link GetLinkByID(int linkKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Link> GetLinks()
        {
            throw new NotImplementedException();
        }

        public void InsertLink(Link link)
        {
            throw new NotImplementedException();
        }

        public void InsertLinks(IEnumerable<Link> links)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
