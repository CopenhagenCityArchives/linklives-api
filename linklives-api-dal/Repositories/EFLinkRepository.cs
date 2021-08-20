using linklives_api_dal.domain;

namespace linklives_api_dal.Repositories
{
    public class EFLinkRepository : EFKeyedRepository<Link>, ILinkRepository
    {
        public EFLinkRepository(LinklivesContext context) : base(context)
        {
        }
    }
}
