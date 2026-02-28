
namespace CollegeStudent.Domain.Entities;

public class Student
{
    public string Id { get; set; } =  string.Empty;
    public  string NamaDepan { get; set; } =  string.Empty;

    public string? NamaBelakang { get; set; }

    public  DateTime TanggalLahir { get; set;}

    public int Usia => (int)Math.Floor((DateTime.UtcNow - TanggalLahir). TotalDays / 365.25 );

}
