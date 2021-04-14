using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFLinkRatingRepository : ILinkRatingRepository
    {
        private readonly LinklivesContext context;

        public EFLinkRatingRepository(LinklivesContext context)
        {
            this.context = context;
        }

        public void DeleteLinkRating(int linkRatingId)
        {
            throw new NotImplementedException();
        }

        public LinkRating GetLinkRatingByID(int linkRatingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LinkRating> GetLinkRatings()
        {
            throw new NotImplementedException();
        }

        public void InsertLinkRating(LinkRating linkRating)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
