using linklives_api_dal.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal
{
    public class LinklivesContext : DbContext
    {
        public DbSet<LifeCourse> LifeCourses { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<LinkRating> LinkRatings { get; set; }
        public LinklivesContext(DbContextOptions<LinklivesContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
