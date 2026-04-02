using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;

namespace SchoolManagement.Infrastructure.Services;

public class SchoolService : ISchoolService
{
    private readonly ApplicationDbContext _context;

    public SchoolService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SchoolDto>> GetAllAsync()
    {
        return await _context.Schools
            .Select(s => new SchoolDto
            {
                Id = s.Id,
                SchoolName = s.SchoolName,
                Address = s.Address,
                City = s.City,
                State = s.State,
                Postcode = s.Postcode
            }).ToListAsync();
    }

    public async Task<SchoolDto> GetByIdAsync(int id)
    {
        var s = await _context.Schools.FindAsync(id);

        if (s == null) return null;

        return new SchoolDto
        {
            Id = s.Id,
            SchoolName = s.SchoolName,
            Address = s.Address,
            City = s.City,
            State = s.State,
            Postcode = s.Postcode
        };
    }

    public async Task<SchoolDto> CreateAsync(SchoolDto dto)
    {
        var school = new School
        {
            SchoolName = dto.SchoolName,
            Address = dto.Address,
            City = dto.City,
            State = dto.State,
            Postcode = dto.Postcode
        };

        _context.Schools.Add(school);
        await _context.SaveChangesAsync();

        dto.Id = school.Id;
        return dto;
    }

    public async Task<SchoolDto> UpdateAsync(int id, SchoolDto dto)
    {
        var school = await _context.Schools.FindAsync(id);
        if (school == null) return null;

        school.SchoolName = dto.SchoolName;
        school.Address = dto.Address;
        school.City = dto.City;
        school.State = dto.State;
        school.Postcode = dto.Postcode;

        await _context.SaveChangesAsync();

        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var school = await _context.Schools.FindAsync(id);
        if (school == null) return false;

        _context.Schools.Remove(school);
        await _context.SaveChangesAsync();

        return true;
    }
}