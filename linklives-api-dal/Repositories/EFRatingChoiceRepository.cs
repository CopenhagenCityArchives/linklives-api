using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFRatingChoiceRepository : IRatingChoiceRepository
    {
        private readonly LinklivesContext context;
        public IEnumerable<RatingChoice> GetAll()
        {
            return context.RatingChoices;
        }

        public RatingChoice GetById(int id)
        {
            return context.RatingChoices.Single(x => x.Id == id);
        }
    }
}
