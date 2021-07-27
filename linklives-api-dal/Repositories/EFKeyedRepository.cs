using linklives_api_dal.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public abstract class EFKeyedRepository<T> : DBRepository<T>  where T : KeyedItem
    {
        protected EFKeyedRepository(LinklivesContext context) : base(context)
        {
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
        public void Insert(IEnumerable<T> entitties)
        {
            var newEntryKeys = entitties.Select(x => x.Key).Distinct().ToArray();
            var keysExiststingInDb = context.Set<T>().Where(x => newEntryKeys.Contains(x.Key)).Select(x => x.Key).ToArray();
            var newEntities = entitties.Where(x => !keysExiststingInDb.Contains(x.Key));

            context.Set<T>().AddRange(newEntities);
        }

    }
}
