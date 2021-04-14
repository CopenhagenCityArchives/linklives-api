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
        IEnumerable<LifeCourse> GetLifeCourses();
        LifeCourse GetLifeCourseByID(int lifeCourseKey);
        void InsertLifeCourses(IEnumerable<LifeCourse> lifeCourses);
        void DeleteLifeCourse(int lifeCourseKey);
        void Save();
    }
}
