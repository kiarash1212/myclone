using GradeBook.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<GradeBookContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("GradeBook") ?? "Data Source=GradeBook.sqlite";
    options.UseSqlite(connectionString);
});

var app = builder.Build();
app.MapControllers();



app.Run();
