using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class MainDbContext :DbContext
    {
        public MainDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollments>()
                .HasOne<Student>(s => s.Students)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(f => f.StudentId)
                .IsRequired();

            modelBuilder.Entity<Enrollments>()
                .HasOne<Course>(c => c.Course)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(f => f.CourseId)
                .IsRequired();
        }
    }
}
