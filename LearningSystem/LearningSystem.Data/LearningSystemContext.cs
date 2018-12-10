namespace LearningSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Identity;

    public class LearningSystemContext : IdentityDbContext<ApplicationUser>
    {
        public LearningSystemContext()
        {
        }

        public LearningSystemContext(DbContextOptions<LearningSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseInstance> CourseInstances { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ResourceType> ResourceTypes { get; set; }

        public DbSet<StudentsInCourses> StudentsInCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.EnrolledCourses)
                .WithOne(ec => ec.Student)
                .HasForeignKey(ec => ec.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CourseInstance>()
                .HasMany(ci => ci.Students)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentsInCourses>()
                .HasKey(sc => new { sc.CourseId, sc.StudentId });

            builder.Entity<HomeworkSubmition>()
                .HasKey(hs => new { hs.AuthorId, hs.LectureId });

            base.OnModelCreating(builder);
        }
    }
}
