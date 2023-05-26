using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Configurations;
using TaskManagerAPI.Contracts.Manager;
using TaskManagerAPI.Contracts.Repository;
using TaskManagerAPI.Contracts.Service;
using TaskManagerAPI.Data;
using TaskManagerAPI.Manager;
using TaskManagerAPI.Repository;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("TaskManagerAPIDbConnectionString");
            services.AddDbContext<DataContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            services.AddAutoMapper(typeof(MapperConfig));

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITaskManager, TaskManager>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                a => a.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
            });

            return services;
        }
    }
}
