using Microsoft.EntityFrameworkCore;
using WebApplicationBackEnd.Entidades;

namespace WebApplicationBackEnd
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Videojuego> Videojuegos { get; set; }
        public DbSet<Datos> Datos { get; set; }
    }
}
