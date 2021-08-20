using linklives_api_dal.domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace linklives_api_dal.Repositories
{
    public class EFLifeCourseRepository : EFKeyedRepository<LifeCourse>, ILifeCourseRepository
    {
        public EFLifeCourseRepository(LinklivesContext context) : base(context)
        {
        }

        public IEnumerable<LifeCourse> GetByUserRatings(string userId)
        {
            var lifecourseskeys = context.LinkRatings.Where(lr => lr.User == userId).Include(x => x.Link.LifeCourse).Select(lr => lr.Link.LifeCourse.Key).Distinct().ToList();
            return this.GetByKeys(lifecourseskeys);
        }
    }
}
