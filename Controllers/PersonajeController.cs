using AlkemyDisney.Models;
using AlkemyDisney.Repositories;
using AlkemyDisney.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlkemyDisney.Controllers
{

    [ApiController]
    [Route("api/characters")]
    [Authorize]
    public class PersonajeController : ControllerBase
    {

        private readonly IPersonajeRepository _personajeRepository;
        private readonly IPeliculaRepository _peliculaRepository;

        public PersonajeController(IPersonajeRepository personajeRepository, IPeliculaRepository peliculaRepository)
        {
            _personajeRepository = personajeRepository;
            _peliculaRepository = peliculaRepository;
        }


        [HttpGet]
        public IActionResult Get(string name, int age, double weight, int movies)
        {

            var characters = _personajeRepository.GetEntities();

            var charactersVM = _personajeRepository.GetPersonajes();


            if (!string.IsNullOrEmpty(name)){
                charactersVM = charactersVM.Where(p => p.Nombre.ToLower().Contains(name.ToLower())).ToList();
            }

            if (age != 0){
                characters = characters.Where(c => c.Edad == age).ToList();

                var charactersVMEdad = new List<GetPersonajesResponseVM>();

                foreach (var character in characters)
                {
                    GetPersonajesResponseVM personajeVM = new GetPersonajesResponseVM 
                    {
                    Imagen = character.Imagen,
                    Nombre = character.Nombre
                    };
                    
                    charactersVMEdad.Add(personajeVM);
                }

                charactersVM = charactersVMEdad;
            }

            if (weight != 0){
                characters = characters.Where(c => c.Peso == weight).ToList();

                var charactersVMPeso = new List<GetPersonajesResponseVM>();

                foreach (var character in characters)
                {
                    GetPersonajesResponseVM personajeVM = new GetPersonajesResponseVM()
                    {
                        Imagen = character.Imagen,
                        Nombre = character.Nombre
                    };

                    charactersVMPeso.Add(personajeVM);
                }

                charactersVM = charactersVMPeso;
            }

            if (movies != 0){

                var peli = _peliculaRepository.GetPelicula(movies);
                if(peli == null)
                {
                    return NotFound("La película de la que desea ver sus personajes no existe");

                }

                var charactersVMPeli = new List<GetPersonajesResponseVM>();

                foreach (var personaje in peli.Personajes)
                {
                    GetPersonajesResponseVM personajeVM = new GetPersonajesResponseVM
                    {
                        Imagen = personaje.Imagen,
                        Nombre = personaje.Nombre
                    };

                    charactersVMPeli.Add(personajeVM);
                }

                charactersVM = charactersVMPeli;
            }  

            if (!charactersVM.Any()) return NoContent();
           
            return Ok(charactersVM);
        }


        [HttpGet]
        [Route("/getCharacter/{id}")]
        public IActionResult Get(int id)
        {
            var personaje = _personajeRepository.GetPersonaje(id);

            if (personaje == null) return BadRequest("El personaje que desea buscar no existe");
            
            return Ok(personaje);
        }


        [HttpPost]
        [Route("/createCharacter")]
        public IActionResult Post(PostPersonajeRequestVM personaje)
        {

            var peliculas = _peliculaRepository.GetEntities();
            var peli = new Pelicula();

            var character = new Personaje()
            {
                Imagen = personaje.Imagen,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Peso = personaje.Peso,
                Historia = personaje.Historia
            };

            if (personaje.Idpeliculas.Any())
            {
                foreach (var id in personaje.Idpeliculas)
                {
                    var pelicula = _peliculaRepository.GetPelicula(id); 
                    if(pelicula == null)
                    {
                        return NotFound("Alguna de las películas que quiere asociar al personaje no existe");
                    }
                    else
                    pelicula.Personajes.Add(character);
                  //  _peliculaRepository.Update(pelicula);
                    character.Peliculas.Add(pelicula);
                }
            }

            _personajeRepository.Add(character);

            return Ok(character);
        }


        [HttpPut]
        [Route("/editCharacter")]
        public IActionResult Put(Personaje personaje)
        {
            var personajeToEdit = _personajeRepository.GetPersonaje(personaje.Id);

            if (personajeToEdit == null) return BadRequest("El personaje que desea modificar no existe");

            personajeToEdit.Imagen = personaje.Imagen;
            personajeToEdit.Nombre = personaje.Nombre;
            personajeToEdit.Edad = personaje.Edad;
            personajeToEdit.Peso = personaje.Peso;
            personajeToEdit.Historia = personaje.Historia;
            personajeToEdit.Peliculas = personaje.Peliculas;

            _personajeRepository.Update(personajeToEdit);
            return Ok(personajeToEdit);
        }


        [HttpDelete]
        [Route("/deleteCharacter/{id}")]
        public IActionResult Delete(int id)
        {
            var personaje = _personajeRepository.GetPersonaje(id);

            if (personaje == null) return BadRequest("El personaje que desea borrar no existe");

            _personajeRepository.Delete(id);
          
            return Ok();
        }


    }

}
