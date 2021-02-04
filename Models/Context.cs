namespace negocio_pequeño.Models
{
    public class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        public Context(Microsoft.EntityFrameworkCore.DbContextOptions<Context> options)
            : base(options)
        {}

        public Microsoft.EntityFrameworkCore.DbSet<Console> Consoles {get;set;}
        public Microsoft.EntityFrameworkCore.DbSet<ConsoleRental> ConsoleRentals { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<MovilDispositivo> MovilDispositivos {get;set;}
        public Microsoft.EntityFrameworkCore.DbSet<InternetRenta> InternetRentas {get;set;}
        public Microsoft.EntityFrameworkCore.DbSet<Product.Product> Product {get;set;}
    }
}