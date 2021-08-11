using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFRatingOptionRepository : DBRepository<RatingOption>, IRatingOptionRepository
    {

        public EFRatingOptionRepository(LinklivesContext context) : base(context)
        {
        }

        public RatingOption GetById(int id)
        {
            return context.RatingOptions.Single(x => x.Id == id);
        }
    }
}
