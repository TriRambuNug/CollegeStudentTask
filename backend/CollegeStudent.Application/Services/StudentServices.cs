using System.Linq.Expressions;
using CollegeStudent.Application.DTOs;
using CollegeStudent.Application.Exceptions;
using CollegeStudent.Application.Interfaces;
using CollegeStudent.Domain.Entities;
using CollegeStudent.Domain.Interfaces;

namespace CollegeStudent.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentAsync(CancellationToken ct = default)
    {
        var students = await _repository.GetAllAsync(ct);
        return students.Select(ToDto);
    }

    public async Task<StudentDto?> GetStudentByIdAsync (string id, CancellationToken ct = default)
    {
        var student = await _repository.GetByIdAsync(id, ct);
        return student is null ? null : ToDto(student);
    }

    public async Task<StudentDto> CreateStudentAsync (CreateStudentDto dto, CancellationToken ct = default)
    {
        if (await _repository.ExistsAsync(dto.Id, ct))
            throw new StudentAlreadyExistsException(dto.Id);

        var student = new Student
        {
            Id = dto.Id,
            NamaDepan = dto.NamaDepan,
            NamaBelakang = dto.NamaBelakang,
            TanggalLahir = dto.TanggalLahir.ToUniversalTime()
        };

        await _repository.AddAsync(student, ct);
        return ToDto(student);
    }

    public async Task<StudentDto> UpdateStudentAsync (string id, UpdateStudentDto dto, CancellationToken ct = default)
    {
        var student = await _repository.GetByIdAsync(id, ct) ?? throw new StudentNotFoundException(id);

        if (!string.Equals(dto.Id, id, StringComparison.OrdinalIgnoreCase))
        {
            if (await _repository.ExistsAsync(dto.Id, ct))
                throw new StudentAlreadyExistsException(dto.Id);

            await _repository.DeleteAsync(id, ct);

            var newStudent = new Student
            {
                Id = dto.Id,
                NamaDepan = dto.NamaDepan,
                NamaBelakang = dto.NamaBelakang,
                TanggalLahir = dto.TanggalLahir.ToUniversalTime()
            };

            await _repository.AddAsync(newStudent, ct);
            return ToDto(newStudent);
        }

        student.NamaDepan = dto.NamaDepan;
        student.NamaBelakang = dto.NamaBelakang;
        student.TanggalLahir = dto.TanggalLahir.ToUniversalTime();

        await _repository.UpdateAsync(student, ct);
        return ToDto(student);
    }

    public async Task DeleteStudentAsync (string id, CancellationToken ct = default)
    {
        if (!await _repository.ExistsAsync(id, ct))
            throw new StudentNotFoundException(id);

        await _repository.DeleteAsync(id, ct);
    }



    private static StudentDto ToDto(Student student) =>
     new(student.Id, student.NamaDepan, student.NamaBelakang ?? string.Empty, student.TanggalLahir, student.Usia);
}