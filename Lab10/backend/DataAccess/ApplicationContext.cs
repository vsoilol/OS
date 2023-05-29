using Lab10.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab10.API.DataAccess;

public class ApplicationContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}