using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaDbContext : DbContext
    {
        internal DbSet<Pizza> Pizzas { get; set; }

        public string connectionString = "Data Source = localhost; Initial Catalog = db_pizzeria; Integrated Security = True;TrustServerCertificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(connectionString).LogTo(s => Debug.WriteLine(s));
    }
}