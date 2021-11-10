using AlkemyDisney.Models;
using AlkemyDisney.Repositories;
using AlkemyDisney.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.Context
{
    [ApiController]
    [Route("api/movies")]
    [Authorize]
    public class PeliculaController : ControllerBase
    {

        private readonly IPeliculaRepository _peliculaRepository;
        private readonly IPersonajeRepository _personajeRepository;
        private readonly IGeneroRepository _generoRepository;
        public PeliculaController (IPeliculaRepository peliculaRepository, IPersonajeRepository personajeRepository,
            IGeneroRepository generoRepository)
        {
            _peliculaRepository = peliculaRepository;
            _personajeRepository = personajeRepository;
            _generoRepository = generoRepository;
        }


        [HttpGet]
        public IActionResult Get(string name, int genre, string order)
        {
            var movies = _peliculaRepository.GetEntities();

            var peliculasVM = _peliculaRepository.GetPeliculas();

            if (!string.IsNullOrEmpty(name)){
                peliculasVM = peliculasVM.Where(m => m.Titulo.ToLower().Contains(name.ToLower())).ToList();
            }

            if(genre != 0) {

                movies = movies.Where(p => p.Genero.Id == genre).ToList();

                if(_generoRepository.GetGenero(genre) == null)
                {
                    return NotFound("El género por el que desea filtrar no existe");
                }

                var peliculasPorgeneroVM = new List<GetPeliculasResponseVM>();
                
                foreach(var movie in movies)
                {
                    GetPeliculasResponseVM peliculaVM = new GetPeliculasResponseVM
                    {
                        Imagen = movie.Imagen,
                        Titulo = movie.Titulo
                    };

                    peliculasPorgeneroVM.Add(peliculaVM);
                }

               peliculasVM = peliculasPorgeneroVM;
            }

            if (!string.IsNullOrEmpty(order) && order == "ASC" || order == "DESC"){
                switch (order){
                    case "ASC":
                        peliculasVM = peliculasVM.OrderBy(m => m.FechaCreacion).ToList();
                        break;
                    case "DESC":
                        peliculasVM = peliculasVM.OrderByDescending(m => m.FechaCreacion).ToList();
                        break;
                }
            }

            if (!peliculasVM.Any()) return NoContent();

            return Ok(peliculasVM);
        }


        [HttpGet]
        [Route("/getMovie/{id}")]
        public IActionResult Get(int id)
        {
            var pelicula = _peliculaRepository.GetPelicula(id);

            if (pelicula == null) return BadRequest("La película que desea buscar no existe");
            
            return Ok(pelicula);
        }


        [HttpPost]
        [Route("/createMovie")]
        public IActionResult Post(PostPeliculaRequestVM pelicula)
        {
            var personajes = _personajeRepository.GetEntities();
            var genero = _generoRepository.GetGenero(pelicula.IdGenero);

            var movie = new Pelicula()
            {
                Imagen = pelicula.Imagen,
                Titulo = pelicula.Titulo,
                FechaCreacion = pelicula.FechaCreacion,
                Calificacion = pelicula.Calificacion,
            };

            if(genero != null)
            {
                genero.Peliculas.Add(movie);
                movie.Genero = genero;
            }

            if (pelicula.IdPersonajes.Any())
            {
                foreach(var id in pelicula.IdPersonajes)
                {
                    var personaje = _personajeRepository.GetPersonaje(id);
                    if (personaje == null)
                    {
                        return NotFound("Alguno de los personajes que quiere asociar a la película no existe");
                    }
                    else
                    personaje.Peliculas.Add(movie);
                    movie.Personajes.Add(personaje);
                }
            }

        _peliculaRepository.Add(movie);
           
            return Ok(movie);
        }


        [HttpPut]
        [Route("/editMovie")]
        public IActionResult Put(Pelicula pelicula)
        {

            var peliculaEdit = _peliculaRepository.GetPelicula(pelicula.Id);

            if (peliculaEdit == null) return BadRequest("La película que desea modificar no existe");

            peliculaEdit.Imagen = pelicula.Imagen;
            peliculaEdit.Titulo = pelicula.Titulo;
            peliculaEdit.FechaCreacion = pelicula.FechaCreacion;
            peliculaEdit.Calificacion = pelicula.Calificacion;
            peliculaEdit.Genero = pelicula.Genero;

            _peliculaRepository.Update(peliculaEdit);
            return Ok(peliculaEdit);
        }


        [HttpDelete]
        [Route("/deleteMovie/{id}")]
        public IActionResult Delete(int id)
        {

            var pelicula = _peliculaRepository.GetPelicula(id);

            if (pelicula  == null) return BadRequest("La película que desea borrar no existe");

            _peliculaRepository.Delete(id);

            return Ok();
        }

    }
}
