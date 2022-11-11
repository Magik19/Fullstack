using FluentAssertions.Common;
using Fullstack.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Conection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors(options =>
    {
        options.WithOrigins(""); //Poner URL
        options.AllowAnyMethod();
        options.AllowAnyHeader();
    });
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();