using linklives_api_dal.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFLinkRatingRepository : EFKeyedRepository<LinkRating>, ILinkRatingRepository
    {
        public EFLinkRatingRepository(LinklivesContext context) : base(context)
        {
        }

        public List<LinkRating> GetbyLinkKey(string linkKey)
        {
            return context.LinkRatings.IncludeAll().Where(x => x.LinkKey == linkKey).ToList();
        }
    }
}
