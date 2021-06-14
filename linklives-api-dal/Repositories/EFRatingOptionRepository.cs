using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFRatingOptionRepository : IRatingOptionRepository
    {
        private readonly LinklivesContext context;
        public IEnumerable<RatingOption> GetAll()
        {
            return context.RatingOptions;
        }

        public RatingOption GetById(int id)
        {
            return context.RatingOptions.Single(x => x.Id == id);
        }
    }
}
