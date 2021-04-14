using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFLifeCourseRepository : ILifeCourseRepository
    {
        private readonly LinklivesContext context;

        public EFLifeCourseRepository(LinklivesContext context)
        {
            this.context = context;
        }

        public void DeleteLifeCourse(string lifeCourseKey)
        {
            throw new NotImplementedException();
        }

        public LifeCourse GetLifeCourseByID(string lifeCourseKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LifeCourse> GetLifeCourses()
        {
            throw new NotImplementedException();
        }

        public void InsertLifeCourse(LifeCourse lifeCourse)
        {
            throw new NotImplementedException();
        }

        public void InsertLifeCourses(IEnumerable<LifeCourse> lifeCourses)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
