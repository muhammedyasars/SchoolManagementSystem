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
            .Include(c => c.TeacherUser)
            .Include(c => c.School)
            .Select(c => new ClassDto
            {
                Id = c.Id,
                ClassName = c.ClassName,
                ClassCode = c.ClassCode,
                TeacherUserId = c.TeacherUserId,
                TeacherFirstName = c.TeacherUser.FirstName,
                TeacherLastName = c.TeacherUser.LastName,
                TeacherEmail = c.TeacherUser.Email,
                SchoolId = c.SchoolId,
                SchoolName = c.School.SchoolName
            }).ToListAsync();
    }

    public async Task<ClassDto> GetByIdAsync(int id)
    {
        var c = await _context.Classes
            .Include(c => c.TeacherUser)
            .Include(c => c.School)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (c == null) return null;

        return new ClassDto
        {
            Id = c.Id,
            ClassName = c.ClassName,
            ClassCode = c.ClassCode,
            TeacherUserId = c.TeacherUserId,
            TeacherFirstName = c.TeacherUser.FirstName,
            TeacherLastName = c.TeacherUser.LastName,
            TeacherEmail = c.TeacherUser.Email,
            SchoolId = c.SchoolId,
            SchoolName = c.School.SchoolName
        };
    }

    public async Task<ClassDto> CreateAsync(ClassDto dto)
    {
        var cls = new Class
        {
            ClassName = dto.ClassName,
            ClassCode = dto.ClassCode,
            TeacherUserId = dto.TeacherUserId,
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
        cls.TeacherUserId = dto.TeacherUserId;
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
