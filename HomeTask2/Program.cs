using HomeTask2.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using HomeTask2.Interfaces;
using HomeTask2.Repository;
using HomeTask2.DataModel;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;

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

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields =
    HttpLoggingFields.RequestHeaders |
    HttpLoggingFields.RequestBody |
    HttpLoggingFields.RequestQuery |
    HttpLoggingFields.ResponseHeaders |
    HttpLoggingFields.ResponseBody |
    HttpLoggingFields.Response;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapControllers();

app.Run();
