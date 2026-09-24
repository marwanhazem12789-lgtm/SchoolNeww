using Microsoft.EntityFrameworkCore;

namespace SCHOOL_MANAGEMENT_API1.Models
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
      public  DbSet<Student> Students { get; set; }
       public DbSet<Subject> Subjects { get; set; }
       public DbSet<Enrollment> Enrollments { get; set; }
       public DbSet<Department> Departments { get; set; }
       public DbSet<ClassRoom> classRooms { get; set; }
       public DbSet<Teacher > Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.StudentId, e.SubjectId })
                .IsUnique();

            modelBuilder.Entity<Student>().HasIndex(t => t.Email).IsUnique();    
            modelBuilder.Entity<Teacher>().HasIndex(t => t.Email).IsUnique();
            

            modelBuilder.Entity<Department>().HasMany(d => d.Teachers)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Teacher>().HasMany(t => t.Subjects)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassRoom>().HasMany(c => c.Students)
                .WithOne(s => s.ClassRoom)
                .HasForeignKey(s => s.ClassRoomId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Subject>().HasMany(s => s.Enrollments)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>().HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Student>().HasKey(Student => Student.Id);
            modelBuilder.Entity<Subject>().HasKey(Subject => Subject.Id);
            modelBuilder.Entity<ClassRoom>().HasKey(ClassRoom => ClassRoom.Id);
            modelBuilder.Entity<Department>().HasKey(Department => Department.Id);
            modelBuilder.Entity<Enrollment>().HasKey(Enrollment => Enrollment.Id);
            modelBuilder.Entity<Teacher>().HasKey(Teacher => Teacher.Id);



            modelBuilder.Entity<Department>().HasData(
              new Department { Id = 1, Name = "Computer Science", Description = "CS Dept" },
              new Department { Id = 2, Name = "Mathematics", Description = "Math Dept" }
          );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "Ahmed",
                    LastName = "Ali",
                    Email = "ahmed.ali@school.com",
                    PhoneNumber = "01012345678",
                    Salary = 8500.0,
                    DepartmentId = 1
                },
                new Teacher
                {
                    Id = 2,
                    FirstName = "Sara",
                    LastName = "Hassan",
                    Email = "sara.hassan@school.com",
                    PhoneNumber = "01123456789",
                    Salary = 9000.0,
                    DepartmentId = 2
                }
            );

            modelBuilder.Entity<ClassRoom>().HasData(
                new ClassRoom { Id = 1, Name = "Class A1", Capacity = 30, GradeLevel = 10 },
                new ClassRoom { Id = 2, Name = "Class B2", Capacity = 25, GradeLevel = 11 }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "C# Programming", Description = "OOP Basics", MaxGrade = 100, TeacherId = 1 },
                new Subject { Id = 2, Name = "Calculus I", Description = "Calculus", MaxGrade = 100, TeacherId = 2 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Omar",
                    LastName = "Khaled",
                    Email = "omar@student.com",
                    PhoneNumber = "01234567890",
                    DateOfBirth = new DateTime(2008, 5, 14),
                    ClassRoomId = 1
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Mona",
                    LastName = "Mahmoud",
                    Email = "mona@student.com",
                    PhoneNumber = "01543216789",
                    DateOfBirth = new DateTime(2007, 9, 20),
                    ClassRoomId = 2
                }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, StudentId = 1, SubjectId = 1, Grade = 85, EnrollmentDate = new DateTime(2025, 9, 1) },
                new Enrollment { Id = 2, StudentId = 2, SubjectId = 2, Grade = 92, EnrollmentDate = new DateTime(2025, 9, 1) }
            );
        }

    }

        
    
}
