using AlkemyDisney.Models;
using AlkemyDisney.ViewModels;
using System.Collections.Generic;

namespace AlkemyDisney.Repositories
{
    public interface IPeliculaRepository : IBaseRepository<Pelicula>
    {
        Pelicula GetPelicula(int id);

        List<GetPeliculasResponseVM> GetPeliculas();
    }
}