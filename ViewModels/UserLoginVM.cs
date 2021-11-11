using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        [MinLength(6)]
        public string userName { get; set; }


        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
