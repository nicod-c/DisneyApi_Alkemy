using AlkemyDisney.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlkemyDisney.Repositories
{
    public class GeneroRepository : BaseRepository<Genero, DisneyDbContext>, IGeneroRepository
    {

        public GeneroRepository(DisneyDbContext dbContext) : base(dbContext)
        {

        }

        public Genero GetGenero(int id)
        {
            return DbSet.Include(g => g.Peliculas).FirstOrDefault(g => g.Id == id);
        }
    }
}
