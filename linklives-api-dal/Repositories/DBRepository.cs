using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public abstract class DBRepository<T> where T : class
    {
        protected readonly LinklivesContext context;

        protected DBRepository(LinklivesContext context)
        {
            this.context = context;
        }
        public int Count()
        {
            return context.Set<T>().Count();
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
