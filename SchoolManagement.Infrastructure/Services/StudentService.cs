using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public StudentService(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<StudentDto>> GetAllAsync()
    {
        return await _context.Students
            .Include(s => s.User)
            .Include(s => s.Class)
            .Select(s => new StudentDto
            {
                Id = s.Id,
                UserId = s.UserId,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName,
                Email = s.User.Email,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                ClassId = s.ClassId,
                ClassName = s.Class.ClassName,
                Points = s.Points,
                IsActive = s.IsActive
            }).ToListAsync();
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var s = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Class)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (s == null) return null;

        return new StudentDto
        {
            Id = s.Id,
            UserId = s.UserId,
            FirstName = s.User.FirstName,
            LastName = s.User.LastName,
            Email = s.User.Email,
            DateOfBirth = s.DateOfBirth,
            Gender = s.Gender,
            ClassId = s.ClassId,
            ClassName = s.Class.ClassName,
            Points = s.Points,
            IsActive = s.IsActive
        };
    }

    public async Task<StudentDto> CreateAsync(StudentDto dto)
    {
        // Check class exists
        var classExists = await _context.Classes.AnyAsync(c => c.Id == dto.ClassId);
        if (!classExists)
            throw new Exception("Invalid ClassId");

        // Check email unique in Users table
        var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (emailExists)
            throw new Exception("Email already exists");

        // Create User first
        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Role = "Student",
            PasswordHash = _passwordHasher.HashPassword("Student@123"), // Default password
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Create Student linked to User
        var student = new Student
        {
            UserId = user.Id,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            ClassId = dto.ClassId,
            Points = 0,
            IsActive = true
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        dto.Id = student.Id;
        dto.UserId = user.Id;
        return dto;
    }

    public async Task<StudentDto> UpdateAsync(int id, StudentDto dto)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student == null) return null;

        student.DateOfBirth = dto.DateOfBirth;
        student.Gender = dto.Gender;
        student.ClassId = dto.ClassId;
        student.IsActive = dto.IsActive;

        // Update User details
        student.User.FirstName = dto.FirstName;
        student.User.LastName = dto.LastName;
        student.User.Email = dto.Email;

        await _context.SaveChangesAsync();

        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student == null) return false;

        _context.Students.Remove(student);
        _context.Users.Remove(student.User);
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddPoints(int studentId, int points)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return false;

        if (!student.IsActive)
            throw new Exception("Student is inactive");

        student.Points += points;  

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SwapStudent(int studentId, int newClassId)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return false;

        var classExists = await _context.Classes.AnyAsync(c => c.Id == newClassId);
        if (!classExists)
            throw new Exception("Target class not found");

        student.ClassId = newClassId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ActivateDeactivate(int id, bool isActive)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;

        student.IsActive = isActive;
        await _context.SaveChangesAsync();

        return true;
    }
}
