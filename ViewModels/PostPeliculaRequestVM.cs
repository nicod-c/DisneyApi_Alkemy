using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.ViewModels
{
    public class PostPeliculaRequestVM
    {
        [Required]
        public string Imagen { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Range(1, 5)]
        public int Calificacion { get; set; }

        public List<int> IdPersonajes { get; set; } = new List<int>();

        public int IdGenero { get; set; }


    }
}
