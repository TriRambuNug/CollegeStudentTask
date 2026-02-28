using CollegeStudent.Domain.Entities;
using CollegeStudent.Domain.Interfaces;
using CollegeStudent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CollegeStudent.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly CollegeStudentDBContext _context;

    public StudentRepository(CollegeStudentDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken ct = default)
         => await _context.Students.AsNoTracking().ToListAsync(ct);

    public async Task<Student?>GetByIdAsync(string id, CancellationToken ct = default)
        => await _context.Students.FindAsync(new object[] {id}, ct);

    public async Task AddAsync (Student student, CancellationToken ct = default)
    {
        await _context.Students.AddAsync(student , ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync (Student student, CancellationToken ct = default)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync (string id, CancellationToken ct = default)
    {
        var student = await _context.Students.FindAsync(new object[] {id}, ct);
        if (student is not null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<bool> ExistsAsync (string id, CancellationToken ct = default)
        => await _context.Students.AnyAsync(s => s.Id == id, ct);


}