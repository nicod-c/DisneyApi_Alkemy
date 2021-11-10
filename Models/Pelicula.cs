using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        public string Imagen { get; set; }

        public string Titulo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int Calificacion { get; set; }

        public List<Personaje> Personajes { get; set; } = new List<Personaje>();

        public Genero Genero { get; set; } = new Genero();

    }
}
