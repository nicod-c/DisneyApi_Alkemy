using AlkemyDisney.Models;
using AlkemyDisney.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.Repositories
{
    public class PersonajeRepository : BaseRepository<Personaje, DisneyDbContext>, IPersonajeRepository
    {

        public PersonajeRepository(DisneyDbContext dbContext) : base(dbContext)
        {

        }

        public Personaje GetPersonaje(int id)
        {
            return DbSet.Include(p => p.Peliculas).ThenInclude(m => m.Genero).FirstOrDefault(p => p.Id == id);
        }

        
        public List<GetPersonajesResponseVM> GetPersonajes()
        {
            var personajes = GetEntities();
            var characters = new List<GetPersonajesResponseVM>();

            foreach (var p in personajes)
            {
                var character = new GetPersonajesResponseVM();
                character.Imagen = p.Imagen;
                character.Nombre = p.Nombre;
                characters.Add(character);
            }

            return characters;
        }
        
    }
}
