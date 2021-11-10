using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.ViewModels
{
    public class PostPersonajeRequestVM
    {
        [Required]
        public string Imagen { get; set; }

        [Required]
        public string Nombre { get; set; }

        public int Edad { get; set; }
        
        public double Peso { get; set; }
        
        public string Historia { get; set; }

        public List<int> Idpeliculas { get; set; } = new List<int>();
    }
}
