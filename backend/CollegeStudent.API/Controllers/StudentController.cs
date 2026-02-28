using CollegeStudent.Application.DTOs;
using CollegeStudent.Application.Exceptions;
using CollegeStudent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CollegeStudent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentController> _logger;

    public StudentController(IStudentService studentService, ILogger<StudentController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }


    //Menampilkan semua data mahasiswa
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var students = await _studentService.GetAllStudentAsync(ct);
        return Ok(students);
    }

    //Menampilkan data mahasiswa berdasarkan id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var student = await _studentService.GetStudentByIdAsync(id, ct);
        return student is null ? NotFound(new { message = "Student not found" }) : Ok(student);
    }

    //Menambahkan data mahasiswa bari
    [HttpPost]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]

    public async Task<IActionResult> Create([FromBody] CreateStudentDto dto, CancellationToken ct)
    {

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try

        {
            var student = await _studentService.CreateStudentAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }
        catch (StudentAlreadyExistsException ex)
        {
            _logger.LogError(ex, "Duplicate student ID: {StudentId}", dto.Id);
            return Conflict(new { message = ex.Message });
        }
    }

    //Memperbarui data mahasiswa
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]

    public async Task<IActionResult> Update(string id, [FromBody] UpdateStudentDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var student = await _studentService.UpdateStudentAsync(id, dto, ct);
            return Ok(student);
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogError(ex, "Student not found: {StudentId}", id);
            return NotFound(new { message = ex.Message });
        }
        catch (StudentAlreadyExistsException ex)
        {
            _logger.LogError(ex, "Duplicate student ID on update: {StudentId}", dto.Id);
            return Conflict(new { message = ex.Message });
        }
    }

    //Menghapus data mahasiswa
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        try
        {
            await _studentService.DeleteStudentAsync(id, ct);
            return NoContent();
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogError(ex, "Student not found: {StudentId}", id);
            return NotFound(new { message = ex.Message });
        }
    }
}
