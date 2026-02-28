using CollegeStudent.Application.Interfaces;
using CollegeStudent.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CollegeStudent.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        return services;
    }
}