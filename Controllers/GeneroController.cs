using AlkemyDisney.Models;
using AlkemyDisney.Repositories;
using AlkemyDisney.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.Controllers
{

    [ApiController]
    [Route("api/genres")]
    [Authorize]
    public class GeneroController : ControllerBase
    {

        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }


        [HttpGet]
        public IActionResult Get(string name, int id)
        {
            var genres = _generoRepository.GetEntities();

            if (!string.IsNullOrEmpty(name))
            {
                genres = genres.Where(g => g.Nombre.ToLower().Contains(name.ToLower())).ToList();
            }

            if(id != 0)
            {
                genres = genres.Where(g => g.Id == id).ToList();
            }

            return Ok(genres);
        }


        [HttpPost]
        [Route("/createGenre")]
        public IActionResult Post(PostGeneroRequestVM generoVM)
        {
            var genero = new Genero()
            {
                Nombre = generoVM.Nombre,
                Imagen = generoVM.Imagen
            };

            _generoRepository.Add(genero);
            return Ok(genero);
        }

        [HttpPut]
        [Route("/editGenre")]
        public IActionResult Put(Genero genero)
        {
            var generoEdit = _generoRepository.GetEntity(genero.Id);

            if (generoEdit == null) return BadRequest("El género que desea modificar no existe");

            generoEdit.Nombre = genero.Nombre;
            generoEdit.Imagen = genero.Imagen;

            _generoRepository.Update(generoEdit);
            return Ok(generoEdit);

        }

        [HttpDelete]
        [Route("/deleteGenre/{id}")]
        public IActionResult Delete(int id)
        {
            var genero = _generoRepository.GetEntity(id);

            if (genero == null) return BadRequest("El género que desea modificar no existe");

            _generoRepository.Delete(id);

            return Ok();
        }

    }
}
