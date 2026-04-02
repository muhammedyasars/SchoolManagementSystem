using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using System.Security.Cryptography;

namespace SchoolManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // RELATIONSHIPS
            modelBuilder.Entity<School>()
                .HasMany(s => s.Classes)
                .WithOne(c => c.School)
                .HasForeignKey(c => c.SchoolId);

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.TeacherUser)
                .WithMany()
                .HasForeignKey(c => c.TeacherUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId);

            // SEED USERS (Teachers and Students)
            var teacher1 = new User { Id = 1, Email = "teacher@test.com", PasswordHash = HashPassword("Password@123"), Role = "Teacher", FirstName = "Ramesh", LastName = "Kumar", IsActive = true };
            var teacher2 = new User { Id = 2, Email = "saritha@test.com", PasswordHash = HashPassword("Password@123"), Role = "Teacher", FirstName = "Saritha", LastName = "Nair", IsActive = true };
            var teacher3 = new User { Id = 3, Email = "manoj@test.com", PasswordHash = HashPassword("Password@123"), Role = "Teacher", FirstName = "Manoj", LastName = "Pillai", IsActive = true };
            
            var studentUser1 = new User { Id = 4, Email = "rahul@test.com", PasswordHash = HashPassword("Student@123"), Role = "Student", FirstName = "Rahul", LastName = "Nair", IsActive = true };
            var studentUser2 = new User { Id = 5, Email = "sneha@test.com", PasswordHash = HashPassword("Student@123"), Role = "Student", FirstName = "Sneha", LastName = "V", IsActive = true };
            var studentUser3 = new User { Id = 6, Email = "arjun@test.com", PasswordHash = HashPassword("Student@123"), Role = "Student", FirstName = "Arjun", LastName = "K", IsActive = true };
            var studentUser4 = new User { Id = 7, Email = "meera@test.com", PasswordHash = HashPassword("Student@123"), Role = "Student", FirstName = "Meera", LastName = "S", IsActive = true };
            var superAdmin = new User { Id = 8, Email = "admin@test.com", PasswordHash = HashPassword("Admin@123"), Role = "SuperAdmin", FirstName = "Super", LastName = "Admin", IsActive = true };

            modelBuilder.Entity<User>().HasData(teacher1, teacher2, teacher3, studentUser1, studentUser2, studentUser3, studentUser4, superAdmin);

            // SEED SCHOOLS
            modelBuilder.Entity<School>().HasData(
                new School { Id = 1, SchoolName = "Kerala Model School", Address = "Trivandrum", City = "TVM", State = "Kerala", Postcode = "695001" },
                new School { Id = 2, SchoolName = "St. Marys High School", Address = "Kochi", City = "EKM", State = "Kerala", Postcode = "682001" }
            );

            // SEED CLASSES
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, ClassName = "Class 10A", ClassCode = "10A-2026", TeacherUserId = 1, SchoolId = 1 },
                new Class { Id = 2, ClassName = "Class 10B", ClassCode = "10B-2026", TeacherUserId = 2, SchoolId = 1 },
                new Class { Id = 3, ClassName = "Class 9A", ClassCode = "9A-2026", TeacherUserId = 3, SchoolId = 2 }
            );

            // SEED STUDENTS
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 4, DateOfBirth = new DateTime(2010, 5, 10), Gender = "Male", ClassId = 1, Points = 10, IsActive = true },
                new Student { Id = 2, UserId = 5, DateOfBirth = new DateTime(2010, 8, 15), Gender = "Female", ClassId = 1, Points = 15, IsActive = true },
                new Student { Id = 3, UserId = 6, DateOfBirth = new DateTime(2010, 1, 20), Gender = "Male", ClassId = 2, Points = 5, IsActive = true },
                new Student { Id = 4, UserId = 7, DateOfBirth = new DateTime(2011, 3, 5), Gender = "Female", ClassId = 3, Points = 20, IsActive = true }
            );
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
