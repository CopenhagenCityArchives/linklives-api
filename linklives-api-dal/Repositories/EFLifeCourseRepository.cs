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
            var lifecourse = context.LifeCourses.Find(lifeCourseKey);
            context.LifeCourses.Remove(lifecourse);
        }

        public LifeCourse GetLifeCourseByID(string lifeCourseKey)
        {
            return context.LifeCourses.Find(lifeCourseKey);
        }

        public IEnumerable<LifeCourse> GetLifeCourses()
        {
            return context.LifeCourses;
        }

        public void InsertLifeCourse(LifeCourse lifeCourse)
        {
            context.LifeCourses.Add(lifeCourse);
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
