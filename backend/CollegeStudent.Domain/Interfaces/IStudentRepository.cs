using CollegeStudent.Domain.Entities;

namespace CollegeStudent.Domain.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync(CancellationToken ct = default);
    Task<Student?> GetByIdAsync(string id, CancellationToken ct = default);

    Task AddAsync (Student student, CancellationToken ct = default);

    Task UpdateAsync (Student student, CancellationToken ct = default);

    Task DeleteAsync (string id, CancellationToken ct = default);

    Task<bool> ExistsAsync(string id, CancellationToken ct = default);
}   

