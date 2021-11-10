using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.ViewModels
{
    public class PostGeneroRequestVM
    {

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Imagen { get; set; }
    }
}
