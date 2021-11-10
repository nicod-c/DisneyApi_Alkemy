using AlkemyDisney.Models;
using AlkemyDisney.ViewModels;
using System.Collections.Generic;

namespace AlkemyDisney.Repositories
{
    public interface IPersonajeRepository : IBaseRepository<Personaje>
    {
        Personaje GetPersonaje(int id);

        List<GetPersonajesResponseVM> GetPersonajes();
    }
}