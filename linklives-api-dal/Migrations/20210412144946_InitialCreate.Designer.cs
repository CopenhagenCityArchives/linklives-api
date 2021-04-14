﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using linklives_api_dal;

namespace linklives_api_dal.Migrations
{
    [DbContext(typeof(LinklivesContext))]
    [Migration("20210412144946_InitialCreate")]
    partial public class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("linklives_api_dal.domain.LifeCourse", b =>
                {
                    b.Property<string>("life_course_key")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("life_course_id")
                        .HasColumnType("int");

                    b.HasKey("life_course_key");

                    b.ToTable("LifeCourses");
                });

            modelBuilder.Entity("linklives_api_dal.domain.Link", b =>
                {
                    b.Property<string>("link_key")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("LifeCourselife_course_key")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("iteration")
                        .HasColumnType("int");

                    b.Property<int>("iteration_inner")
                        .HasColumnType("int");

                    b.Property<int>("link_id")
                        .HasColumnType("int");

                    b.Property<string>("method_description")
                        .HasColumnType("text");

                    b.Property<int>("method_id")
                        .HasColumnType("int");

                    b.Property<string>("method_subtype1")
                        .HasColumnType("text");

                    b.Property<string>("method_type")
                        .HasColumnType("text");

                    b.Property<int>("pa_id1")
                        .HasColumnType("int");

                    b.Property<int>("pa_id2")
                        .HasColumnType("int");

                    b.Property<double>("score")
                        .HasColumnType("double");

                    b.Property<int>("source_id1")
                        .HasColumnType("int");

                    b.Property<int>("source_id2")
                        .HasColumnType("int");

                    b.HasKey("link_key");

                    b.HasIndex("LifeCourselife_course_key");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("linklives_api_dal.domain.LinkRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Description")
                        .HasColumnType("int");

                    b.Property<string>("link_key")
                        .HasColumnType("varchar(767)");

                    b.Property<bool>("rating")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("link_key");

                    b.ToTable("LinkRatings");
                });

            modelBuilder.Entity("linklives_api_dal.domain.Link", b =>
                {
                    b.HasOne("linklives_api_dal.domain.LifeCourse", "LifeCourse")
                        .WithMany("Links")
                        .HasForeignKey("LifeCourselife_course_key");

                    b.Navigation("LifeCourse");
                });

            modelBuilder.Entity("linklives_api_dal.domain.LinkRating", b =>
                {
                    b.HasOne("linklives_api_dal.domain.Link", "Link")
                        .WithMany()
                        .HasForeignKey("link_key");

                    b.Navigation("Link");
                });

            modelBuilder.Entity("linklives_api_dal.domain.LifeCourse", b =>
                {
                    b.Navigation("Links");
                });
#pragma warning restore 612, 618
        }
    }
}
