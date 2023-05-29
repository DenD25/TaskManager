using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class DataContext : IdentityDbContext<User, Role, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(x => x.Photo)
                .WithOne(x => x.User)
                .HasForeignKey<Photo>(x => x.UserId);

            builder.Entity<Photo>()
                .HasOne(x => x.User)
                .WithOne(x => x.Photo)
                .HasForeignKey<User>(x => x.PhotoId);

            builder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });

            builder.Entity<ProjectUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UserId);

            builder.Entity<ProjectUser>()
                .HasOne(x => x.Project)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ProjectId);

            builder.Entity<TaskModel>()
                .HasOne(x => x.AssignedTo)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TaskModel>()
                .HasOne(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
