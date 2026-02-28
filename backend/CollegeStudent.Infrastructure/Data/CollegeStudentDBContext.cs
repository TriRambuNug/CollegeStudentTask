using CollegeStudent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeStudent.Infrastructure.Data;

public class CollegeStudentDBContext : DbContext
{
    public CollegeStudentDBContext(DbContextOptions<CollegeStudentDBContext> options) : base(options) { }
    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);
    
            entity.Property(s => s.Id)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(s => s.NamaDepan)
                .HasMaxLength(100)
                .IsRequired();
            
            entity.Property(s => s.NamaBelakang)
                .HasMaxLength(100);

            entity.Property(s => s.TanggalLahir)
                .HasColumnType("timestamptz")
                .IsRequired();

            entity.Ignore(s => s.Usia);
        });
    }


}