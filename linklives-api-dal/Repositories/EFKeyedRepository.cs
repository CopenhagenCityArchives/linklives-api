using linklives_api_dal.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public abstract class EFKeyedRepository<T> where T : KeyedItem
    {
        protected readonly LinklivesContext context;

        protected EFKeyedRepository(LinklivesContext context)
        {
            this.context = context;
        }
        public void Delete(string key)
        {
            var entity = context.Set<T>().Find(key);
            context.Set<T>().Remove(entity);
        }
        public T GetByKey(string key)
        {
            return context.Set<T>().IncludeAll().SingleOrDefault(x => x.Key == key);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Insert(IEnumerable<T> entitties)
        {
            context.Set<T>().AddRange(entitties);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
