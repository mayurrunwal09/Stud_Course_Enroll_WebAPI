using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services.Custom.CourseServices;
using Infrastructure.Services.Custom.EnrollementServices;
using Infrastructure.Services.Custom.StudentServices;
using Infrastructure.Services.Generic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MainDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddCors(c =>
{
    c.AddPolicy("AlloWOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>),typeof(Service<>));
builder.Services.AddTransient(typeof(IStudentService),typeof(StudentService));
builder.Services.AddTransient(typeof(ICourseService), typeof(CourseService));
builder.Services.AddTransient(typeof(IEnrollementService), typeof(EnrollementService));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
