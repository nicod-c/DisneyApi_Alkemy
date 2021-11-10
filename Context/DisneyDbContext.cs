using Microsoft.EntityFrameworkCore;

namespace AlkemyDisney.Models
{
    public class DisneyDbContext : DbContext
    {
        public DisneyDbContext()
        {

        }

        public DisneyDbContext(DbContextOptions<DisneyDbContext> options) : base(options)
        {
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pelicula> PeliculasYSeries { get; set; }
        public DbSet<Personaje> Personajes { get; set; }

      
    }

}
