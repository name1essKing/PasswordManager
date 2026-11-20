using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Client.Services; 
using PasswordManager.Server.Data;
using PasswordManager.Server.Mappings;
using PasswordManager.Server.Services;
using PasswordManager.Server.Services.API;
using PasswordManager.Server.Services.API.Interfaces;
using PasswordManager.Server.Services.Interfaces;
using PasswordManager.Server.UnitOfWork;

namespace PasswordManager.Server;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureDbContexts(services);
        ConfigureMappers(services);
        ConfigureApplicationServices(services);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
    }

    private void ConfigureDbContexts(IServiceCollection services)
    {
        Console.WriteLine($"SERVER WORKING DIRECTORY: {Directory.GetCurrentDirectory()}");
        Console.WriteLine($"DATABASE PATH: {Path.GetFullPath("passwords.db")}");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=passwords.db"));
    }

    private void ConfigureMappers(IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
        }, AppDomain.CurrentDomain.GetAssemblies());
    }

    private void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddSingleton<IPasswordGeneratorService, PasswordGeneratorService>();
        services.AddScoped<IPasswordsRepository, PasswordsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IPasswordsService, PasswordsService>();
    }

}