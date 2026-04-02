using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllAsync();
    Task<StudentDto> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(StudentDto dto);
    Task<StudentDto> UpdateAsync(int id, StudentDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> AddPoints(int studentId, int points);
    Task<bool> SwapStudent(int studentId, int newClassId);

    Task<bool> ActivateDeactivate(int id, bool isActive);
}