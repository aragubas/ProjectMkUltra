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
        
        // Add Use Cases
        builder.Services.AddScoped<LoginUserCase>();
        builder.Services.AddScoped<GetUsersUseCase>();
        builder.Services.AddScoped<CreateUserUseCase>();
        builder.Services.AddScoped<DeleteUserUseCase>();
        builder.Services.AddScoped<PatchUserUseCase>();

        // Add Controllers
        builder.Services.AddControllers();

        builder.Services.AddDbContext<MkApiDatabase>();

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
