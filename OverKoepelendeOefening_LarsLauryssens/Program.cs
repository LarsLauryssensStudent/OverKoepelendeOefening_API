using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OverKoepelendeOefening_LarsLauryssens.Data;

var builder = WebApplication.CreateBuilder(args);

//De dbcontext zetten
var database = builder.Configuration.GetConnectionString("Default");
Console.WriteLine(database);
if (database == null)
{
    Console.WriteLine("Geen variabele ingegeven voor de database");
}
else if (database == "SQL")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLserver"));
    });
    Console.WriteLine("Using SQL-Server database");
}
else
{
    Console.WriteLine("Oops er ging iets fout");
}

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
     });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
