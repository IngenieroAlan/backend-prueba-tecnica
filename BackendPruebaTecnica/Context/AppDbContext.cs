using BackendPruebaTecnica.Models;
using Microsoft.EntityFrameworkCore;
namespace BackendPruebaTecnica.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definimos UserName y Email como únicos
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Definimos PhoneNumber y Email como únicos
            modelBuilder.Entity<Contact>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "server=127.0.0.1;port=3306;user=root;password=1234;database=PruebaTecnicaDB";
            var connectionString = "server=bsxwwpodxtirrblzezrt-mysql.services.clever-cloud.com;port=3306;user=un9idbjxtag0h90e;password=KYx9tYQvNXaCucX7PfMc;database=bsxwwpodxtirrblzezrt";
            var serverVersion = new MySqlServerVersion(new Version(8,0,31));
            optionsBuilder.UseMySql(connectionString, serverVersion)
                //Opciones de desarrollador
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }*/
    }
}
