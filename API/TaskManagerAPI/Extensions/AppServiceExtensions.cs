using ApplicationCore.Configurations;
using ApplicationCore.Contracts.Manager;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Service;
using ApplicationCore.Manager;
using ApplicationCore.Repository;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            services.AddScoped<IPhotoService,PhotoService>();

            services.Configure<CloudinaryConfig>(config.GetSection("CloudnarySettings"));

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
