﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using linklives_api_dal;

namespace linklives_api_dal.Migrations
{
    [DbContext(typeof(LinklivesContext))]
    partial class LinklivesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("linklives_api_dal.domain.LifeCourse", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("Life_course_id")
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.ToTable("LifeCourses");
                });

            modelBuilder.Entity("linklives_api_dal.domain.Link", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("Iteration")
                        .HasColumnType("int");

                    b.Property<int>("Iteration_inner")
                        .HasColumnType("int");

                    b.Property<string>("LifeCourseKey")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("Link_id")
                        .HasColumnType("int");

                    b.Property<string>("Method_description")
                        .HasColumnType("text");

                    b.Property<int>("Method_id")
                        .HasColumnType("int");

                    b.Property<string>("Method_subtype1")
                        .HasColumnType("text");

                    b.Property<string>("Method_type")
                        .HasColumnType("text");

                    b.Property<int>("Pa_id1")
                        .HasColumnType("int");

                    b.Property<int>("Pa_id2")
                        .HasColumnType("int");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<int>("Source_id1")
                        .HasColumnType("int");

                    b.Property<int>("Source_id2")
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.HasIndex("LifeCourseKey");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("linklives_api_dal.domain.LinkRating", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("LinkKey")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("RatingId")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.HasIndex("LinkKey");

                    b.HasIndex("RatingId");

                    b.ToTable("LinkRatings");
                });

            modelBuilder.Entity("linklives_api_dal.domain.RatingOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Heading")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RatingOptions");
                });

            modelBuilder.Entity("linklives_api_dal.domain.Link", b =>
                {
                    b.HasOne("linklives_api_dal.domain.LifeCourse", null)
                        .WithMany("Links")
                        .HasForeignKey("LifeCourseKey");
                });

            modelBuilder.Entity("linklives_api_dal.domain.LinkRating", b =>
                {
                    b.HasOne("linklives_api_dal.domain.Link", null)
                        .WithMany("Ratings")
                        .HasForeignKey("LinkKey");

                    b.HasOne("linklives_api_dal.domain.RatingOption", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("linklives_api_dal.domain.LifeCourse", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("linklives_api_dal.domain.Link", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
