using Microsoft.EntityFrameworkCore;

namespace CoursesAndStudents
{
    public class Academy : DbContext
    {
        DbSet<Student>? Students { get; set; }
        DbSet<Course>? Courses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "Data Source=.;Database=academy;Integrated Security=true;MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(s => s.LastName).HasMaxLength(30).IsRequired();

            Student alice = new()
            {
                StudentId = 1,
                FirstName = "Alice",
                LastName = "Jones"
            };

            Student bob = new()
            {
                StudentId = 2,
                FirstName = "Bob",
                LastName = "Smith"
            };

            Student cecilia = new()
            {
                StudentId = 3,
                FirstName = "Cecilia",
                LastName = "Ramirez"
            };

            Course csharp = new()
            {
                CourseId = 1,
                Title = "C#"
            };

            Course webdev = new()
            {
                CourseId = 2,
                Title = "Web Development"
            };

            Course python = new()
            {
                CourseId = 3,
                Title = "Python"
            };

            modelBuilder.Entity<Student>().HasData(alice, bob, cecilia);
            modelBuilder.Entity<Course>().HasData(csharp, webdev, python);
            modelBuilder.Entity<Course>().HasMany<Student>(c => c.Students).WithMany(c => c.Courses).UsingEntity(e => e.HasData(
                new { CoursesCourseId = 1, StudentsStudentId = 1 },
                new { CoursesCourseId = 1, StudentsStudentId = 2 },
                new { CoursesCourseId = 1, StudentsStudentId = 3 },
                new { CoursesCourseId = 2, StudentsStudentId = 2 },
                new { CoursesCourseId = 3, StudentsStudentId = 3 }
            ));
        }
    }
}