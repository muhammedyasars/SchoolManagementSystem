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
            .Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                ClassId = s.ClassId,
                Points = s.Points,
                IsActive = s.IsActive
            }).ToListAsync();
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

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var s = await _context.Students.FindAsync(id);
        if (s == null) return null;

        return new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            DateOfBirth = s.DateOfBirth,
            Gender = s.Gender,
            ClassId = s.ClassId,
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

        // Check email unique
        var emailExists = await _context.Students.AnyAsync(s => s.Email == dto.Email);
        if (emailExists)
            throw new Exception("Email already exists");

        var student = new Student
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            ClassId = dto.ClassId,
            Points = 0,
            IsActive = true
        };

        // Student-nu login cheyyaan 'User' table-il record create cheyyunnu
        var user = new User
        {
            Email = dto.Email,
            // Default password: Student@123
            PasswordHash = _passwordHasher.HashPassword("Student@123"),
            Role = "Student",
            IsActive = true
        };

        _context.Students.Add(student);
        _context.Users.Add(user);
        
        await _context.SaveChangesAsync();

        dto.Id = student.Id;
        return dto;
    }

    public async Task<StudentDto> UpdateAsync(int id, StudentDto dto)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return null;

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.Email = dto.Email;
        student.DateOfBirth = dto.DateOfBirth;
        student.Gender = dto.Gender;
        student.ClassId = dto.ClassId;

        await _context.SaveChangesAsync();

        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;

        _context.Students.Remove(student);
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