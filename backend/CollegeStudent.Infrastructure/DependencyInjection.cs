using CollegeStudent.Domain.Interfaces;
using CollegeStudent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollegeStudent.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<CollegeStudentDBContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IStudentRepository, Repositories.StudentRepository>();
        return services;
    }
    
}