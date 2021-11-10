using AlkemyDisney.Models;

namespace AlkemyDisney.Repositories
{
    public interface IGeneroRepository : IBaseRepository<Genero>
    {
        Genero GetGenero(int id);
    }
}