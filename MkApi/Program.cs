using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using MkApi.Application.UseCases;
using MkApi.Database;
using MkApi.Domain.Repositories;
using MkApi.Repositories;
namespace MkApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add Services
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<LoginUserCase>();

        // Add Controllers
        builder.Services.AddControllers();
        
        string? connectionString = builder.Configuration.GetConnectionString("Default");
        if (connectionString == null)
            throw new InvalidOperationException("Connection string 'Default' not found");

        builder.Services.AddDbContext<MkApiDatabase>(options => {
            options.UseSqlServer(connectionString);
        });

        var app = builder.Build();


        // Only enable swagger when in development
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Map controllers
        app.MapControllers();

        app.Run();
    }
}
