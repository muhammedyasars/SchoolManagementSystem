using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Interfaces;

public interface ISchoolService
{
    Task<List<SchoolDto>> GetAllAsync();
    Task<SchoolDto> GetByIdAsync(int id);
    Task<SchoolDto> CreateAsync(SchoolDto dto);
    Task<SchoolDto> UpdateAsync(int id, SchoolDto dto);
    Task<bool> DeleteAsync(int id);
}