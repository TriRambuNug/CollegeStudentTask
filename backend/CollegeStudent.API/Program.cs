
using CollegeStudent.Application;
using CollegeStudent.Infrastructure;
using CollegeStudent.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { Title = "CollegeStudent.API",
      Version = "v1",
      Description = "API for managing college students." 
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if(File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "host=localhost;port=5432;database=CollegeStudentDB;username=postgres;password=yourpassword";

builder.Services
    .AddApplication()
    .AddInfrastructure(connectionString);


builder.Services.AddCors(opts =>
    opts.AddPolicy("AllowSPA", policy =>
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();
    
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CollegeStudentDBContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CollegeStudent.API v1");
        c.RoutePrefix = "swagger";
    });

}

app.UseCors("AllowSPA");
app.UseAuthorization();
app.MapControllers();

app.Run();