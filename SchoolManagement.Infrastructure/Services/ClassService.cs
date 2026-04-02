using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;

namespace SchoolManagement.Infrastructure.Services;

public class ClassService : IClassService
{
    private readonly ApplicationDbContext _context;

    public ClassService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClassDto>> GetAllAsync()
    {
        return await _context.Classes
            .Select(c => new ClassDto
            {
                Id = c.Id,
                ClassName = c.ClassName,
                ClassCode = c.ClassCode,
                TeacherFirstName = c.TeacherFirstName,
                TeacherLastName = c.TeacherLastName,
                TeacherEmail = c.TeacherEmail,
                SchoolId = c.SchoolId
            }).ToListAsync();
    }

    public async Task<ClassDto> GetByIdAsync(int id)
    {
        var c = await _context.Classes.FindAsync(id);
        if (c == null) return null;

        return new ClassDto
        {
            Id = c.Id,
            ClassName = c.ClassName,
            ClassCode = c.ClassCode,
            TeacherFirstName = c.TeacherFirstName,
            TeacherLastName = c.TeacherLastName,
            TeacherEmail = c.TeacherEmail,
            SchoolId = c.SchoolId
        };
    }

    public async Task<ClassDto> CreateAsync(ClassDto dto)
    {
        var cls = new Class
        {
            ClassName = dto.ClassName,
            ClassCode = dto.ClassCode,
            TeacherFirstName = dto.TeacherFirstName,
            TeacherLastName = dto.TeacherLastName,
            TeacherEmail = dto.TeacherEmail,
            SchoolId = dto.SchoolId
        };

        _context.Classes.Add(cls);
        await _context.SaveChangesAsync();

        dto.Id = cls.Id;
        return dto;
    }

    public async Task<ClassDto> UpdateAsync(int id, ClassDto dto)
    {
        var cls = await _context.Classes.FindAsync(id);
        if (cls == null) return null;

        cls.ClassName = dto.ClassName;
        cls.ClassCode = dto.ClassCode;
        cls.TeacherFirstName = dto.TeacherFirstName;
        cls.TeacherLastName = dto.TeacherLastName;
        cls.TeacherEmail = dto.TeacherEmail;
        cls.SchoolId = dto.SchoolId;

        await _context.SaveChangesAsync();

        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cls = await _context.Classes.FindAsync(id);
        if (cls == null) return false;

        _context.Classes.Remove(cls);
        await _context.SaveChangesAsync();

        return true;
    }
}