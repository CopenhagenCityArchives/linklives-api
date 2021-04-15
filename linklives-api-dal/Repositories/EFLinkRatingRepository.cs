using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFLinkRatingRepository : BaseEFRepository<LinkRating>, ILinkRatingRepository
    {
        public EFLinkRatingRepository(LinklivesContext context) : base(context)
        {
        }
        public new LinkRating GetByKey(string key)
        {
            //This is the only one of our domain object that doesnt have a string as its primary key
            throw new NotImplementedException();
        }

        public LinkRating GetById(int linkRatingId)
        {
            return context.LinkRatings.Find(linkRatingId);
        }
    }
}
