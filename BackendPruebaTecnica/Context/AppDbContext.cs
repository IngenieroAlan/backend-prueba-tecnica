using BackendPruebaTecnica.Models;
using Microsoft.EntityFrameworkCore;
namespace BackendPruebaTecnica.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=127.0.0.1;port=3306;user=root;password=1234;database=PruebaTecnicaDB";
            var serverVersion = new MySqlServerVersion(new Version(8,0,31));
            optionsBuilder.UseMySql(connectionString, serverVersion)
                //Opciones de desarrollador
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        /*public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }*/
    }
}
