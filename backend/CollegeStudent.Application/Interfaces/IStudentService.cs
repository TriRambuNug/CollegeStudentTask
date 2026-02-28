using CollegeStudent.Application.DTOs;

namespace CollegeStudent.Application.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllStudentAsync(CancellationToken ct = default);
    Task<StudentDto?> GetStudentByIdAsync(string id, CancellationToken ct = default);
    Task <StudentDto> CreateStudentAsync(CreateStudentDto dto, CancellationToken ct = default);
    Task <StudentDto> UpdateStudentAsync(string id, UpdateStudentDto dto, CancellationToken ct = default);
    Task DeleteStudentAsync(string id, CancellationToken ct = default);
}