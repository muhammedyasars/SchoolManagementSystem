using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Interfaces;

public interface IClassService
{
    Task<List<ClassDto>> GetAllAsync();
    Task<ClassDto> GetByIdAsync(int id);
    Task<ClassDto> CreateAsync(ClassDto dto);
    Task<ClassDto> UpdateAsync(int id, ClassDto dto);
    Task<bool> DeleteAsync(int id);
}