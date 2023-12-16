using TVPrograms.Code;
using TVPrograms.Data;
using TVPrograms.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<HttpClientCommand>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Scaffold-DbContext "Host=localhost;Port=5432;Database=LimeHdTv;Username=postgres;Password=ihesop69" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir "Data" -f
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
