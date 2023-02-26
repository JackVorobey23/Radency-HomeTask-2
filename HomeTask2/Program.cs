using HomeTask2.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using HomeTask2.Interfaces;
using HomeTask2.Repository;
using HomeTask2.DataModel;
using System.Reflection;
using HomeTask2.Dto;
using FluentValidation.AspNetCore;
using FluentValidation;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataBaseContext>((db) => db.UseInMemoryDatabase("Library").UseLazyLoadingProxies());

builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddHttpLogging(opt => opt.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestQuery);

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
