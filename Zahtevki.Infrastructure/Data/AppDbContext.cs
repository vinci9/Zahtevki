using Microsoft.EntityFrameworkCore;
using Zahtevki.Core.Entities;

namespace Zahtevki.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TasksEntity> Tasks { get; set; }   
    }
}