using MappyApplication.Data.Configuration;
using MappyApplication.Models.workouts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MappyApplication.Data;

public class MappyDBContext : IdentityDbContext<User>
{
    public MappyDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Cities> Cities { get; set; }
    public DbSet<Workouts> Workouts { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.Entity<Workouts>().Property(p => p.Type).HasConversion(new EnumToStringConverter<WorkoutsType>());
        
    }
}