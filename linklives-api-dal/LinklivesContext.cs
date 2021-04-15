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

            modelBuilder.Entity<LifeCourse>(entity =>
            {
                entity.HasKey(x => x.Key);
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.HasKey(x => x.Key);
                entity.HasOne(x => x.LifeCourse).WithMany(x => x.Links).HasForeignKey(x => x.LifeCourseKey);
            });

            modelBuilder.Entity<LinkRating>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Link).WithMany(x => x.Ratings).HasForeignKey(x => x.LinkKey);
            });

        }
    }
}
