using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public interface ILifeCourseRepository
    {
        IEnumerable<LifeCourse> GetAll();
        LifeCourse GetByKey(string lifeCourseKey);
        IEnumerable<LifeCourse> GetByUserRatings(string userId);
        void Insert(LifeCourse lifeCourse);
        void Insert(IEnumerable<LifeCourse> lifeCourses);
        void Delete(string lifeCourseKey);
        void Save();
    }
}
