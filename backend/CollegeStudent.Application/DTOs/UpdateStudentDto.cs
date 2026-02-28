using System.ComponentModel.DataAnnotations;
namespace CollegeStudent.Application.DTOs;

public record UpdateStudentDto (
    [Required(ErrorMessage = "Nomor Induk Mahasiswa harus diisi.")]
    [StringLength(20, MinimumLength = 10)]
    string Id,

    [Required(ErrorMessage = "Nama depan harus diisi.")]
    [StringLength(100)]
    string NamaDepan,

    [StringLength(100)]
    string? NamaBelakang,

    [Required(ErrorMessage = "Tanggal lahir harus diisi.")]
    DateTime TanggalLahir
);