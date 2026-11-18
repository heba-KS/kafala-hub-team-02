using System;
using System.Collections.Generic;
using System.Text;
using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DalilAcademy.Infra.Data
{


    public class AppDbContext(
        DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<Course> Courses => Set<Course>();


        public DbSet<Student> Students => Set<Student>();

        public DbSet<Instructor> Instructors
            => Set<Instructor>();

        public DbSet<Enrollment> Enrollments
            => Set<Enrollment>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    

    

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((context, _) =>
            {
                // Add students only if none exist.
                if (!context.Set<Student>().Any())
                {
                    context.Set<Student>().AddRange(
                        new Student
                        {
                            FullName = "Ahmad Ali",
                            Email = "ahmad@example.com",
                            PhoneNumber = "0790000001"
                        },
                        new Student
                        {
                            FullName = "Lina Omar",
                            Email = "lina@example.com",
                            PhoneNumber = "0790000002"
                        },
                        new Student
                        {
                            FullName = "Sara Khaled",
                            Email = "sara@example.com",
                            PhoneNumber = "0790000003"
                        });

                    context.SaveChanges();
                }

                // Add instructors and courses if no instructors exist.
                if (!context.Set<Instructor>().Any())
                {
                    var ahmad = new Instructor
                    {
                        FullName = "Ahmad Saleh"
                    };

                    var lina = new Instructor
                    {
                        FullName = "Lina Haddad"
                    };

                    context.Set<Instructor>().AddRange(ahmad, lina);
                    context.SaveChanges();

                    if (!context.Set<Course>().Any())
                    {
                        context.Set<Course>().AddRange(
                            new Course
                            {
                                Title = "C# Fundamentals",
                                Price = 50,
                                InstructorId = ahmad.Id
                            },
                            new Course
                            {
                                Title = "ASP.NET Core Web API",
                                Price = 80,
                                InstructorId = ahmad.Id
                            },
                            new Course
                            {
                                Title = "PostgreSQL Essentials",
                                Price = 60,
                                InstructorId = lina.Id
                            });


                            // Existing instructor/course seeding
                     
                    }
                }
            });
        }
    } }
