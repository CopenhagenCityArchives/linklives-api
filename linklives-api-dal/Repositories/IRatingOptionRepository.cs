using linklives_api_dal.domain;
using System.Collections.Generic;

namespace linklives_api_dal.Repositories
{
    public interface IRatingOptionRepository
    {
        IEnumerable<RatingOption> GetAll();
        RatingOption GetById(int id);
    }
}
