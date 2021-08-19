﻿using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public interface ILifeCourseRepository
    {
        int Count();
        IEnumerable<LifeCourse> GetAll();
        LifeCourse GetByKey(string lifeCourseKey);
        IEnumerable<LifeCourse> GetByKeys(IList<string> lifecourseskeys);
        IEnumerable<LifeCourse> GetByUserRatings(string userId);
        void Insert(LifeCourse lifeCourse);
        void Insert(IEnumerable<LifeCourse> lifeCourses);
        void Upsert(IEnumerable<LifeCourse> entitties);
        void Delete(string lifeCourseKey);
        void Save();
    }
}
