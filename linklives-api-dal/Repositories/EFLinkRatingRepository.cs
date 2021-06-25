using linklives_api_dal.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class EFLinkRatingRepository : ILinkRatingRepository
    {
        private readonly LinklivesContext context;

        public EFLinkRatingRepository(LinklivesContext context)
        {
            this.context = context;
        }
        public List<LinkRating> GetbyLinkKey(string linkKey)
        {
            return context.LinkRatings.IncludeAll().Where(x => x.LinkKey == linkKey).ToList();
        }
        public void Delete(int id)
        {
            var entity = context.LinkRatings.Find(id);
            context.LinkRatings.Remove(entity);
        }
        public LinkRating GetById(int id)
        {
            return context.LinkRatings.IncludeAll().SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<LinkRating> GetAll()
        {
            return context.LinkRatings;
        }

        public void Insert(LinkRating entity)
        {
            context.LinkRatings.Add(entity);
        }

        public void Insert(IEnumerable<LinkRating> entitties)
        {
            context.LinkRatings.AddRange(entitties);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
