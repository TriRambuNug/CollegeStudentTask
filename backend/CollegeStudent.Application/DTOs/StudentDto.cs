namespace CollegeStudent.Application.DTOs;

public record StudentDto(
    string Id,
    string NamaDepan,
    string NamaBelakang,
    DateTime TanggalLahir,
    int Usia
);
    

