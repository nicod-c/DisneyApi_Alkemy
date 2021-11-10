using AlkemyDisney.Models;
using AlkemyDisney.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.Repositories
{
    public class PeliculaRepository : BaseRepository<Pelicula, DisneyDbContext>, IPeliculaRepository
    {

        public PeliculaRepository(DisneyDbContext dbContext) : base(dbContext)
        {

        }


        public Pelicula GetPelicula(int id)
        {
            return DbSet.Include(m => m.Personajes).Include(m => m.Genero).Where(m => m.Id == id).FirstOrDefault();
        }

        public List<GetPeliculasResponseVM> GetPeliculas()
        {
            var peliculas = GetEntities();
            var movies = new List<GetPeliculasResponseVM>();

            foreach (var p in peliculas)
            {
                var movie = new GetPeliculasResponseVM();
                movie.Imagen = p.Imagen;
                movie.Titulo = p.Titulo;
                movie.FechaCreacion = p.FechaCreacion;
                movies.Add(movie);
            }

            return movies;
        }
    }

}
