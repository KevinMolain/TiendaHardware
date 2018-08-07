using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareShop.Models
{
    public class Account
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Usuario { get; set; }
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])")]
        public string Contraseña { get; set; }
        [Required]
        public string Correo { get; set; }
    }
}
